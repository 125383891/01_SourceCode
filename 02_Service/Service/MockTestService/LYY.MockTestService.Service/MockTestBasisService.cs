using LYY.MockTest.Entity.Entity;
using LYY.MockTest.Entity.Enums;
using LYY.MockTest.Entity.ViewModel;
using LYY.MockTestService.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Utility;

namespace LYY.MockTestService.Service
{
    public class MockTestBasisService
    {
        #region 题库管理
        /// <summary>
        /// 查询问题列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<QusetionView> SearchQuestionPageLists(QusetionFilter filter)
        {
            return MockTestDA.SearchQuestionPageLists(filter);
        }


        /// <summary>
        /// 读取问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QusetionView LoadQustion(int id)
        {
            var qustion = MockTestDA.LoadQustion(id);
            qustion.AnswerList = MockTestDA.SearchAnswerLists(id);
            return qustion;
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="entity"></param>
        public string UpdateQustion(QusetionView entity)
        {
            if (entity.id > 0)
            {
                entity.update_time = DateTime.Now;
                List<DocumentValid> document = new List<DocumentValid>();
                var msg = DocumentValidMsg(entity.document_name, entity.order_num + "", document);
                if (!string.IsNullOrEmpty(msg) && (document.Count == 0 || document.FirstOrDefault().QuestionId != entity.qusetionId))
                {
                    return msg;
                }
                entity.document_id = document.FirstOrDefault().id;
                MockTestDA.UpdateQustion(entity);
                foreach (var item in entity.AnswerList)
                {
                    MockTestDA.UpdateAnswer(item);
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userid"></param>
        public void InsertQusetion(List<QusetionView> list, string userid)
        {
            foreach (var item in list)
            {
                item.create_time = DateTime.Now;
                item.create_user = userid;
                var id = MockTestDA.InsertQustion(item);
                foreach (var answer in item.AnswerList)
                {
                    answer.question_id = id;
                    MockTestDA.InsertAnswer(answer);
                }
            }
        }


        public void ImportQusetion(DataTable data, List<DocumentValid> documentList, List<DocumentValid> userTagList, string userid)
        {
            List<QusetionView> list = new List<QusetionView>();
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                //查询关联文档 判断关联文档与序号是否存在
                QusetionView qusetion = new QusetionView
                {
                    //关联文档标题
                    document_name = row[1] + "",
                    //题目序号
                    order_num = int.Parse(row[2] + ""),
                    //题目文本内容
                    content = row[3] + ""
                };
                //文档id
                qusetion.document_id = documentList.Where(_ => _.name.Equals(qusetion.document_name)).FirstOrDefault().id;
                var usertagname = row[0] + "";
                var userTagid = userTagList.Where(_ => _.name.Equals(usertagname)).FirstOrDefault().user_tag;
                qusetion.user_tag = userTagid;
                //题型
                switch (int.Parse(row[4] + ""))
                {
                    case 1:
                        qusetion.mode = Question_Mode.Single;
                        break;
                    case 2:
                        qusetion.mode = Question_Mode.Multiple;
                        break;
                    case 3:
                        qusetion.mode = Question_Mode.Judge;
                        break;
                    default:
                        break;
                }
                qusetion.answer_analysis = string.IsNullOrEmpty(row[5] + "") ? "" :row[5]+"";
                //IP限制
                qusetion.is_ip_restrict = string.IsNullOrEmpty(row[6] + "") ? 0 : int.Parse(row[6] + "");
                //用户限制
                qusetion.is_user_restrict = string.IsNullOrEmpty(row[7] + "") ? 0 : int.Parse(row[7] + "");
                //正确答案
                string is_rightStr = row[8] + "";
                List<AnswerView> answerList = new List<AnswerView>();
                AddAnswer(row, answerList, is_rightStr);
                qusetion.AnswerList = answerList;
                list.Add(qusetion);
            }
            InsertQusetion(list, userid);

        }
        /// <summary>
        /// 添加答案 最多9个答案 从索引8开始 编号从大写A开始
        /// </summary>
        /// <param name="row"></param>
        /// <param name="answerList"></param>
        /// <param name="is_rightStr"></param>
        private void AddAnswer(object[] row, List<AnswerView> answerList, string is_rightStr)
        {
            //第8列开始是答案项
            int columnIndex = 9;
            ASCIIEncoding asciiEncoding = new ASCIIEncoding();
            for (int i = 0; i < 9; i++)
            {
                if (row.Length > (columnIndex + i) && !string.IsNullOrEmpty(row[columnIndex + i] + ""))
                {
                    byte[] byteArray = new byte[] { (byte)(i + 65) };
                    string strCharacter = asciiEncoding.GetString(byteArray);
                    answerList.Add(new AnswerView()
                    {
                        content = row[columnIndex + i] + "",
                        label = strCharacter,
                        is_right = is_rightStr.IndexOf((i + 1).ToString()) >= 0 ? Answer_Is_Right.Yes : Answer_Is_Right.Not

                    });
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        /// <param name="data"></param>
        public string EmptyValid(DataTable data)
        {
            string errorMsg = "";
            int rowIndex = 1;
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                for (int i = 0; i < row.Length; i++)
                {
                    //大于9 后面的答案不必须
                    if (i == 5 || i == 6 || i == 7 || i > 10)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(row[i] + ""))
                    {
                        errorMsg += string.Format("{0}行{1}列为空<br/>", rowIndex, i + 1);
                    }
                }
                var count = data.Select("关联文档标题='" + row[0] + "' and 题目序号='" + row[1] + "'").Count();
                if (count > 1)
                {
                    errorMsg += string.Format("{0}行关联文档标题和题目序号与其他行存在重复<br/>", rowIndex);
                }
                rowIndex++;
            }
            return errorMsg;
        }

        public string UserTagValid(DataTable data, List<DocumentValid> usertagList)
        {
            string errorMsg = "";
            int rowIndex = 1;
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                //0列为用户类型
                var usertag = row[0] + "";
                var msg = UserTagValidMsg(usertag, usertagList);
                if (!string.IsNullOrEmpty(msg))
                {
                    errorMsg += string.Format("{0}行" + msg, rowIndex);
                }
                rowIndex++;
            }
            return errorMsg;
        }

        public string UserTagValidMsg(string name, List<DocumentValid> usertagList)
        {
            string errorMsg = "";
            var document = MockTestDA.LoadUserTagValid(name);
            if (document == null)
            {
                errorMsg = "用户类型查找不到<br/>";

            }
            if (document != null)
            {
                usertagList.Add(document);
            }
            return errorMsg;
        }

        public string DocumentValid(DataTable data, List<DocumentValid> documentList)
        {
            string errorMsg = "";
            int rowIndex = 1;
            foreach (System.Data.DataRow item in data.Rows)
            {
                var row = item.ItemArray;
                var documentname = row[1] + "";
                var order_num = row[2] + "";
                var msg = DocumentValidMsg(documentname, order_num, documentList);
                if (!string.IsNullOrEmpty(msg))
                {
                    errorMsg += string.Format("{0}行" + msg, rowIndex);
                }
                rowIndex++;
            }
            return errorMsg;
        }

        public string DocumentValidMsg(string documentname, string order_num, List<DocumentValid> documentList)
        {
            string errorMsg = "";
            var document = MockTestDA.LoadDocumentValid(documentname, int.Parse(order_num));
            if (document == null)
            {
                errorMsg = "关联文档标题查找不到<br/>";

            }
            else if (document.QuestionId.HasValue)
            {
                errorMsg = "关联文档与题目序号已存在<br/>";
            }
            if (document != null)
            {
                documentList.Add(document);
            }
            return errorMsg;
        }
        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public int DeleteQuestion(int sysno)
        {
            var result = MockTestDA.DeleteQuestion(sysno);
            return result;
        }
        #endregion
        #region 考试配置管理
        /// <summary>
        /// 考试配置分页查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<ExamView> SearchExamPageLists(ExamFilter filter)
        {
            return MockTestDA.SearchExamPageLists(filter);
        }
        /// <summary>
        /// 读取考试配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamView LoadExam(int id)
        {
            var exam = MockTestDA.LoadExam(id);
            exam.DocumentList = MockTestDA.SearchExamDocumentLists(id);
            return exam;
        }

        /// <summary>
        /// 保存考试配置
        /// </summary>
        /// <param name="entity"></param>
        public string SaveExam(ExamView entity)
        {
            //验证文档是否存在
            List<t_document> documentList = new List<t_document>();
            if (!string.IsNullOrEmpty(entity.documentStrSave))
            {
                var documentArr = entity.documentStrSave.Split('\n').Where(_ => !string.IsNullOrEmpty(_)).ToList();
                #region 验证文档
                string message = "";
                foreach (var item in documentArr)
                {
                    var document = MockTestDA.LoadDocumentByName(item, entity.user_tag);
                    if (document == null)
                    {
                        message += string.Format("{0}未找到对应用户类型的文档信息<br />", item);
                    }
                    else
                    {
                        documentList.Add(document);
                    }
                }
                if (!string.IsNullOrEmpty(message))
                {
                    return message;
                }
                #endregion
            }

            if (entity.id > 0)
            {
                entity.update_time = DateTime.Now;
                MockTestDA.UpdateExam(entity);
                MockTestDA.DeleteExamDocument(entity.id);
                foreach (var item in documentList)
                {
                    MockTestDA.InsertExamDocument(new ExamDocumentView()
                    {
                        document_id = item.id,
                        exam_id = entity.id
                    });
                }
            }
            else
            {
                //新增考试配置关联文档
                entity.create_time = DateTime.Now;
                entity.create_user = entity.update_user;
                var id = MockTestDA.InsertExam(entity);
                foreach (var item in documentList)
                {
                    MockTestDA.InsertExamDocument(new ExamDocumentView()
                    {
                        document_id = item.id,
                        exam_id = id
                    });
                }
            }
            return "";
        }
        #endregion
        #region 测验历史记录

        /// <summary>
        /// 测验历史记录分页查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public QueryResult<ExamTestView> SearchExamTestPageLists(ExamTestFilter filter)
        {
            var data = MockTestDA.SearchExamTestPageLists(filter);
            return data;
        }

        /// <summary>
        /// 测验历史记录分不分页查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<ExamTestView> ExportExamTestList(ExamTestFilter filter)
        {
            var data = MockTestDA.ExportExamTestList(filter);
            return data;
        }
        /// <summary>
        /// 查询模拟考试  下拉数据源
        /// </summary>
        /// <returns></returns>
        public List<t_exam> SearchExamLists()
        {
            var data = MockTestDA.SearchExamLists();
            return data;
        }

        #endregion

        #region 测验统计
        /// <summary>
        /// 测验统计查询/导出 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<TestStatisticsView> SearchTestStatisticsLists(TestStatisticsFilter filter)
        {
            var data = MockTestDA.SearchTestStatisticsLists(filter);
            return data;
        }
        #endregion


        #region 段位参数设置
        public QueryResult<RankSettingVO> SearchRankSettingPageLists(RankSettingFilter filter)
        {
            var data = MockTestDA.SearchRankSettingPageLists(filter);
            return data;
        }

        public RankSettingVO LoadRankSettingDetail(int id)
        {
            var data = MockTestDA.LoadRankSettingDetail(id);
            return data;
        }

        public int UpdateRankSetting(RankSettingVO entity)
        {
            return MockTestDA.UpdateRankSetting(entity);
        }
        #endregion


        #region 个人积分
        public List<IndividualPointsVO> SearchIndividualPointsLists(IndividualPointsFilter filter)
        {
            return MockTestDA.SearchIndividualPointsLists(filter);
        }
        #endregion

        #region 团队积分
        public List<TeamPointsVO> SearchTeamPointsLists(TeamPointsFilter filter)
        {
            return MockTestDA.SearchTeamPointsLists(filter);
        }
        #endregion
    }
}
