using AutoMapper;
using LYY.Common.Entity.Enums;
using YZ.Utility;
namespace LYY.Document.Entity
{
    public class DocumentComboVO
    {
        public int Id { get; set; }
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public DocumentEnums? Type { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        public string TypeDesc
        {
            get { if (Type.HasValue) { return Type.GetDescription(); } else { return string.Empty; } }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtensionName { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }


        public bool? IsStudyMaterials { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserTagEnums? Usertag { get; set; }

        public string UsertagDesc
        {
            get { if (Type.HasValue) { return Usertag.GetDescription(); } else { return string.Empty; } }
        }
        /// <summary>
        /// 需要学习时间
        /// </summary>
        public int? Minminutes { get; set; }

        public int OrderNum { get; set; }
    }

    public static class DocumentComboConvert
    {
        private static MapperConfiguration config;
        static DocumentComboConvert()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FolderVO, DocumentComboVO>()
                    .BeforeMap((src, dest) => { dest.Type = DocumentEnums.Directory; })
                    .BeforeMap((src, dest) => { dest.ExtensionName = "--"; })
                    .BeforeMap((src, dest) => { dest.Size = "--"; });
                cfg.CreateMap<DocumentVO, DocumentComboVO>()
                    .ForMember(dest => dest.Type, src => src.Ignore()) //由于名称重名，所以先过滤ComboVO中的Type字段
                    .BeforeMap((src, dest) => { dest.Type = DocumentEnums.File; })
                    .ForMember(d => d.ExtensionName, t => t.MapFrom(x => x.Type))
                    .ForMember(d => d.Size, t => t.MapFrom(x => x.Size.ToString()))
                    .ForMember(d => d.Usertag, t => t.MapFrom(x => x.Usertag))
                    .ForMember(d => d.Id, t => t.MapFrom(x => x.Id))
                    .ForMember(d => d.Minminutes, t => t.MapFrom(x => x.Minminutes))
                    .ForMember(d => d.OrderNum, t => t.MapFrom(x => x.OrderNum));
            });
        }

        public static DocumentComboVO ToDocumentComboVO(this FolderVO data)
        {
            return config.CreateMapper().Map<FolderVO, DocumentComboVO>(data);
        }

        public static DocumentComboVO ToDocumentComboVO(this DocumentVO data)
        {
            return config.CreateMapper().Map<DocumentVO, DocumentComboVO>(data);
        }
    }
}
