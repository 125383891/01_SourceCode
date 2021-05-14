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
    public class DocumentBasisService
    {
        private FolderBasisService FolderBasisService { get { return new FolderBasisService(); } }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="document">数据实体</param>
        public void Insert(DocumentVO document)
        {
            // check
            DocumentEntity entity = document.ToDocumentEntity();
            this.CheckEntity(entity);
            var folder = FolderBasisService.GetById(entity.FolderId);
            if (folder == null)
            {
                throw new BusinessException("目录不存在，不允许再上传文件");
            }
            DocumentDA.Insert(entity);
        }

        public void Update(DocumentEntity entity)
        {
            this.CheckEntity(entity);
            DocumentDA.Update(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">id</param>
        public void Delete(int id)
        {
            DocumentDA.Delete(new DocumentEntity() { Id = id, IsDeleted = DeletedEnums.Deleted });
        }

        /// <summary>
        /// 根据目录ID获取数据列表
        /// </summary>
        /// <returns>目录数据列表</returns>
        public List<DocumentVO> ListByFolderId(int? folderId, int? user_tag)
        {
            return DocumentDA.ListByFolderId(folderId, user_tag).Select(p => p.ToDocumentVo()).ToList();
        }

        public DocumentEntity DocumentById(int id)
        {
            return DocumentDA.DocumentById(id);
        }

        /// <summary>
        /// 检查实体
        /// </summary>
        /// <param name="folderEntity">实体校验</param>
        private void CheckEntity(DocumentEntity documentEntity)
        {
            if (string.IsNullOrEmpty(documentEntity.Name))
            {
                throw new BusinessException("文件名称不允许为空");
            }
            else if (documentEntity.Name.Length > 255)
            {
                throw new BusinessException("文件名称最大长度255");
            }
            else if (string.IsNullOrEmpty(documentEntity.Type))
            {
                throw new BusinessException("文件类型不允许为空");
            }
            else if (documentEntity.Type.Length > 100)
            {
                throw new BusinessException("文件类型最大长度100");
            }
            else if (string.IsNullOrEmpty(documentEntity.Url))
            {
                throw new BusinessException("文件Url不允许为空");
            }
            else if (documentEntity.Url.Length > 3000)
            {
                throw new BusinessException("url最大长度3000");
            }
            else if (!string.IsNullOrEmpty(documentEntity.Words) && documentEntity.Words.Length > 1000)
            {
                throw new BusinessException("关键词最大长度1000");
            }
            int n = DocumentDA.ValidUserTagAndName(documentEntity);
            if (n>0)
            {
                throw new BusinessException("在同一种用户类型中，文件名称不可与其他文件重复");
            }
        }
    }
}
