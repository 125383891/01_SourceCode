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
    public class DocumentDA
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="document">数据实体</param>
        public static void Insert(DocumentEntity document)
        {
            var cmd = new DataCommand("Document.Insert");
            cmd.SetParameter(document);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="document">数据实体</param>
        public static void Update(DocumentEntity document)
        {
            var cmd = new DataCommand("Document.Update");
            cmd.SetParameter(document);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="document">数据实体</param>
        public static void Delete(DocumentEntity document)
        {
            var cmd = new DataCommand("Document.Delete");
            cmd.SetParameter(document);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 根据目录ID获取数据列表
        /// </summary>
        /// <returns>目录数据列表</returns>
        public static List<DocumentEntity> ListByFolderId(int? folderId, int? user_tag)
        {
            var cmd = new DataCommand("Document.ListByFolderId");
            cmd.SetParameter("@FolderId", DbType.Int32, folderId);
            if (user_tag.HasValue)
            {
                cmd.QuerySetCondition("user_tag", ConditionOperation.Equal, DbType.Int32, user_tag.Value);
            }
            return cmd.ExecuteEntityList<DocumentEntity>("#STRWHERE#");
        }
        /// <summary>
        /// 根据目录ID获取数据列表
        /// </summary>
        /// <returns>目录数据列表</returns>
        public static DocumentEntity DocumentById(int id)
        {
            var cmd = new DataCommand("Document.DocumentById");
            cmd.SetParameter("@id", DbType.Int32, id);
            return cmd.ExecuteEntity<DocumentEntity>();
        }

        /// <summary>
        /// 根据目录ID获取数据列表
        /// </summary>
        /// <returns>目录数据列表</returns>
        public static int ValidUserTagAndName(DocumentEntity documentEntity)
        {
            var cmd = new DataCommand("Document.ValidUserTagAndName");
            cmd.QuerySetCondition("user_tag", ConditionOperation.Equal, DbType.Int32, documentEntity.Usertag.Value);
            cmd.QuerySetCondition("name", ConditionOperation.Equal, DbType.String, documentEntity.Name);

            if (documentEntity.Id.HasValue && documentEntity.Id > 0)
            {
                cmd.QuerySetCondition("id", ConditionOperation.NotEqual, DbType.Int32, documentEntity.Id.Value);
            }
            var result=cmd.ExecuteEntity<DocumentEntity>("#STRWHERE#");
            return result.Id.Value;
        }


    }
}
