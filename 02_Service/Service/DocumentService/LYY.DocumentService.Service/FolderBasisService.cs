using LYY.Common.Entity;
using LYY.Document.Entity;
using LYY.DocumentService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.DocumentService.Service
{
    public class FolderBasisService
    {
        private DocumentBasisService DocumentBasisService { get { return new DocumentBasisService(); } }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="folder">数据实体</param>
        public void Insert(FolderVO folder)
        {
            FolderEntity entity = folder.ToFolderEntity();
            this.CheckEntity(entity);
            if (!entity.ParentId.HasValue)
            {
                throw new BusinessException("父节点Id不允许为空");
            }
            else if (CheckParentIsDeleted(entity.ParentId.Value))
            {
                throw new BusinessException("父节点已被删除,请重新刷新页面进行操作");
            }
            FolderDA.Insert(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="folder">数据实体</param>
        public void Update(FolderVO folder)
        {
            // check
            FolderEntity entity = folder.ToFolderEntity();
            this.CheckEntity(entity);
            FolderDA.Update(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="folder">数据实体</param>
        public void Delete(int id)
        {
            FolderEntity entity = FolderDA.GetById(id);
            if (entity == null)
            {
                throw new BusinessException("数据不存在无法删除");
            }
            bool hasChildren = this.ListByParentId(entity.Id).Any();
            if (hasChildren)
            {
                throw new BusinessException(string.Format("{0}目录下存在子目录，不允许删除该目录", entity.Name));
            }
            bool hasDocument = DocumentBasisService.ListByFolderId(entity.Id, null).Any();
            if (hasDocument)
            {
                throw new BusinessException(string.Format("{0}目录下存在文件，不允许删除该目录", entity.Name));
            }
            FolderDA.Delete(new FolderEntity() { Id = entity.Id, IsDeleted = DeletedEnums.Deleted });
        }

        /// <summary>
        /// 根据Id获取目录数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回目录数据</returns>
        public FolderVO GetById(int id)
        {
            return FolderDA.GetById(id).ToFolderVo();
        }

        /// <summary>
        /// 查询目录列表
        /// </summary>
        /// <returns>目录数据列表</returns>
        public List<FolderVO> List()
        {
            return FolderDA.List().Select(p => p.ToFolderVo()).ToList();
        }

        /// <summary>
        /// 根据父级ID目录数据
        /// </summary>
        /// <returns>目录数据列表</returns>
        public List<FolderVO> ListByParentId(int? parentId)
        {
            return FolderDA.ListByParentId(parentId).Select(p => p.ToFolderVo()).ToList();
        }

        /// <summary>
        /// 检查父节点是否被删除
        /// </summary>
        /// <param name="parentId">父节点id</param>
        /// <returns>true/false</returns>
        public bool CheckParentIsDeleted(int parentId)
        {
            return FolderDA.GetById(parentId) == null;
        }

        /// <summary>
        /// 检查实体
        /// </summary>
        /// <param name="folderEntity">实体校验</param>
        private void CheckEntity(FolderEntity folderEntity)
        {
            if (string.IsNullOrEmpty(folderEntity.Name))
            {
                throw new BusinessException("请输入目录名称");
            }
            else if (folderEntity.Name.Length > 255)
            {
                throw new BusinessException("目录名称最大长度255");
            }
        }
    }
}
