using LYY.Document.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility.DataAccess;

namespace LYY.DocumentService.DataAccess
{
    public class FolderDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="folder">数据实体</param>
        public static void Insert(FolderEntity folder)
        {
            var cmd = new DataCommand("Folder.Insert");
            cmd.SetParameter(folder);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="folder">数据实体</param>
        public static void Update(FolderEntity folder)
        {
            var cmd = new DataCommand("Folder.Update");
            cmd.SetParameter(folder);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="folder">数据实体</param>
        public static void Delete(FolderEntity folder)
        {
            var cmd = new DataCommand("Folder.Delete");
            cmd.SetParameter(folder);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 根据Id获取目录数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回目录数据</returns>
        public static FolderEntity GetById(int id)
        {
            var cmd = new DataCommand("Folder.GetById");
            cmd.SetParameter("@Id", DbType.Int32, id);
            return cmd.ExecuteEntity<FolderEntity>();
        }

        /// <summary>
        /// 查询目录列表
        /// </summary>
        /// <returns>目录数据列表</returns>
        public static List<FolderEntity> List()
        {
            var cmd = new DataCommand("Folder.List");
            return cmd.ExecuteEntityList<FolderEntity>();
        }

        /// <summary>
        /// 根据父级ID目录数据
        /// </summary>
        /// <returns>目录数据列表</returns>
        public static List<FolderEntity> ListByParentId(int? parentId)
        {
            var cmd = new DataCommand("Folder.ListByParentId");
            cmd.SetParameter("@ParentId", DbType.Int32, parentId);
            return cmd.ExecuteEntityList<FolderEntity>();
        }
    }
}
