using MQWebApplication.Models;

namespace MQWebApplication.DBhelpler;

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public partial interface IGetxml
{
    #region Methods

    /// <summary>
    /// 生成机构信息
    /// </summary>
    /// <param name="department"></param>
    /// <returns></returns>
    public string CreateSampleESBXmljg(Department department);

    /// <summary>
    /// 人员信息
    /// </summary>
    /// <param name="department"></param>
    /// <returns></returns>
    public string CreateSampleESBXmlry(StaffInfoDetailed staff);

    public string GenerateESBXml(Getxml.ESBEntry entry);

    /// <summary>
    /// 病区信息
    /// </summary>
    /// <param name="department"></param>
    /// <returns></returns>
    public string CreateSampleESBXmlBQ(Departments department);

    #endregion Methods
}

public class Getxml
{
    // 数据模型定义
    [XmlRoot("ESBEntry")]
    public class ESBEntry
    {
        [XmlElement("AccessControl")] public AccessControl? AccessControl { get; set; }

        [XmlElement("MessageHeader")] public MessageHeader? MessageHeader { get; set; }

        [XmlElement("MsgInfo")] public MsgInfo? MsgInfo { get; set; }
    }

    public class AccessControl
    {
        [XmlElement("SysFlag")] public int SysFlag { get; set; }

        [XmlElement("UserName")] public string? UserName { get; set; }

        [XmlElement("Password")] public string? Password { get; set; }

        [XmlElement("Fid")] public string? Fid { get; set; }

        [XmlElement("OrderNo")] public string? OrderNo { get; set; }
    }

    public class MessageHeader
    {
        [XmlElement("OrderNo")] public string? OrderNo { get; set; }

        [XmlElement("Fid")] public string? Fid { get; set; }

        [XmlElement("SourceSysCode")] public string? SourceSysCode { get; set; }

        [XmlElement("ReturnFlag")] public int ReturnFlag { get; set; }

        [XmlElement("HospCode")] public string? HospCode { get; set; }

        [XmlElement("TargetSysCode")] public string? TargetSysCode { get; set; }

        [XmlElement("MsgDate")] public string? MsgDate { get; set; }
    }

    public class MsgInfo
    {
        [XmlAttribute("flag")] public string? Flag { get; set; }

        [XmlElement("Msg")] public Msg? Msg { get; set; }
    }

    public class Msg
    {
        [XmlAttribute("id")] public string? Id { get; set; }

        [XmlAttribute("lastUpdate")] public string? LastUpdate { get; set; }

        [XmlAttribute("action")] public string? Action { get; set; }


        [XmlElement("RECORD_DATE")] public string? RecordDate { get; set; }

        [XmlElement("SUBOR_HOSPITAL_DISTRICT")]
        public string? SuborHospitalDistrict { get; set; }

        [XmlElement("DEPT_CODE")] public string? DeptCode { get; set; }

        [XmlElement("DEPT_NAME")] public string? DeptName { get; set; }

        [XmlElement("OI_DEPT_FLAG")] public string? OiDeptFlag { get; set; }

        [XmlElement("MS_DEPT_FLAG")] public string? MsDeptFlag { get; set; }

        [XmlElement("DEPT_CATEG_CODE")] public string? DeptCategCode { get; set; }

        [XmlElement("DEPT_CATEG_NAME")] public string? DeptCategName { get; set; }

        [XmlElement("SUPERIOR_DEPT_CODE")] public string? SuperiorDeptCode { get; set; }

        [XmlElement("SUPERIOR_DEPT_NAME")] public string? SuperiorDeptName { get; set; }

        [XmlElement("LOCATION")] public string? Location { get; set; }

        [XmlElement("CLINIC_DEPT_FLAG")] public string? ClinicDeptFlag { get; set; }

        [XmlElement("IS_WARD")] public string? IsWard { get; set; }

        [XmlElement("WUBI_CODE")] public string? WubiCode { get; set; }

        [XmlElement("UPDATE_USER")] public string? UpdateUser { get; set; }

        [XmlElement("UPDATE_TIME")] public string? UpdateTime { get; set; }

        [XmlElement("UPDATE_FLAG")] public string? UpdateFlag { get; set; }

        [XmlElement("TITLE_LEVEL_NAME")] public string? TitleLevelName { get; set; }

        [XmlElement("TITLE_LEVEL_CODE")] public string? TitleLevelCode { get; set; }

        [XmlElement("SUBOR_DEPT_NAME")] public string? SuborDeptName { get; set; }

        [XmlElement("SUBOR_DEPT_CODE")] public string? SuborDeptCode { get; set; }

        [XmlElement("STAFF_NAME")] public string? StaffName { get; set; }

        [XmlElement("STAFF_CODE")] public string? StaffCode { get; set; }

        [XmlElement("STAFF_CATEG_NAME")] public string? StaffCategName { get; set; }

        [XmlElement("STAFF_CATEG_CODE")] public string? StaffCategCode { get; set; }

        [XmlElement("STAFF_BRIEFING")] public string? StaffBriefing { get; set; }

        [XmlElement("ROLE_AUTHORITY_NAME")] public string? RoleAuthorityName { get; set; }

        [XmlElement("ROLE_AUTHORITY_CODE")] public string? RoleAuthorityCode { get; set; }

        [XmlElement("POLITICAL_DESC")] public string? PoliticalDesc { get; set; }

        [XmlElement("PINYIN_CODE")] public string? PinyinCode { get; set; }

        [XmlElement("PHYSI_SEX_NAME")] public string? PhysiSexName { get; set; }

        [XmlElement("PHYSI_SEX_CODE")] public string? PhysiSexCode { get; set; }

        [XmlElement("PHONE_NO_OFFICE")] public string? PhoneNoOffice { get; set; }

        [XmlElement("PHONE_NO_HOME")] public string? PhoneNoHome { get; set; }

        [XmlElement("PHONE_NO")] public string? PhoneNo { get; set; }

        [XmlElement("NATIVE_ADDR")] public string? NativeAddr { get; set; }

        [XmlElement("NATION_NAME")] public string? NationName { get; set; }

        [XmlElement("NATION_CODE")] public string? NationCode { get; set; }

        [XmlElement("MARITAL_STATUS_NAME")] public string? MaritalStatusName { get; set; }

        [XmlElement("MARITAL_STATUS_CODE")] public string? MaritalStatusCode { get; set; }

        [XmlElement("MAJOR_SKILL_POST_NAME")] public string? MajorSkillPostName { get; set; }

        [XmlElement("MAJOR_SKILL_POST_CODE")] public string? MajorSkillPostCode { get; set; }

        [XmlElement("LOGIN_PASSWORD")] public string? LoginPassword { get; set; }

        [XmlElement("LOGIN_NAME")] public string? LoginName { get; set; }

        [XmlElement("INVALID_FLAG")] public string? InvalidFlag { get; set; }

        [XmlElement("ID_NUMBER")] public string? IdNumber { get; set; }

        [XmlElement("HUKOU_ADDR")] public string? HukouAddr { get; set; }

        [XmlElement("ETHNIC_NAME")] public string? EthnicName { get; set; }

        [XmlElement("ETHNIC_CODE")] public string? EthnicCode { get; set; }

        [XmlElement("EMAIL")] public string? Email { get; set; }

        [XmlElement("EDUCATION_NAME")] public string? EducationName { get; set; }

        [XmlElement("EDUCATION_CODE")] public string? EducationCode { get; set; }

        [XmlElement("DATE_BIRTH")] public string? DateBirth { get; set; }

        [XmlElement("CURR_ADDR")] public string? CurrAddr { get; set; }

        [XmlElement("CREATE_USER")] public string? CreateUser { get; set; }

        [XmlElement("CREATE_TIME")] public string? CreateTime { get; set; }
    }

    public class ESBXmlGenerator : IGetxml
    {
        // 生成 XML 的方法
        public string GenerateESBXml(ESBEntry entry)
        {
            try
            {
                // 创建 XML 序列化器
                var serializer = new XmlSerializer(typeof(ESBEntry));

                // 设置 XML 序列化设置
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", ""); // 移除默认命名空间

                // 配置 XmlWriterSettings 以去掉 XML 声明和换行
                var settings = new XmlWriterSettings
                {
                    Indent = false, // 禁用缩进
                    OmitXmlDeclaration = true, // 去掉 XML 声明
                    Encoding = new UTF8Encoding(false), // 设置编码为 UTF-8
                    NewLineHandling = NewLineHandling.None, // 不处理换行符
                    NewLineChars = "", // 将换行符设置为空
                    ConformanceLevel = ConformanceLevel.Document
                };

                // 使用 MemoryStream 替代 StringWriter
                using (var memoryStream = new MemoryStream())
                using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
                {
                    // 序列化对象到 XML
                    serializer.Serialize(xmlWriter, entry, namespaces);
                    // 将流转换为字符串
                    return Encoding.UTF8.GetString(memoryStream.ToArray()).TrimStart('\uFEFF');
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成 XML 失败: " + ex.Message);
            }
        }

        // 生成机构 
        public string CreateSampleESBXmljg(Department department)
        {
            var entry = new ESBEntry
            {
                AccessControl = new AccessControl
                {
                    SysFlag = 1,
                    UserName = "rsgl",
                    Password = "rsgl",
                    Fid = "PS02004",
                    OrderNo = "PS02004S01001"
                },
                MessageHeader = new MessageHeader
                {
                    OrderNo = "PS02004S01001",
                    Fid = "PS02004",
                    SourceSysCode = "S27",
                    ReturnFlag = -1,
                    HospCode = "GH01",
                    TargetSysCode = "S00",
                    MsgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                },
                MsgInfo = new MsgInfo
                {
                    Flag = "",
                    Msg = new Msg
                    {
                        Id = "1",
                        LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Action = "update",
                        PinyinCode = department.PinyinCode!,
                        WubiCode = department.WubiCode!,
                        InvalidFlag = department.InvalidFlag!,
                        RecordDate = string.IsNullOrWhiteSpace(Convert.ToString(department.RecordDate!))
                            ? ""
                            : Convert.ToString(department.RecordDate!),
                        SuborHospitalDistrict = department.SuborHospitalDistrict!,
                        DeptCode = department.DeptCode!,
                        DeptName = department.DeptName!,
                        OiDeptFlag = department.OiDeptFlag!,
                        MsDeptFlag = department.MsDeptFlag!,
                        DeptCategCode = department.DeptCategCode!,
                        DeptCategName = department.DeptCategName!,
                        SuperiorDeptCode = department.SuperiorDeptCode!,
                        SuperiorDeptName = department.SuperiorDeptName!,
                        Location = department.Location!,
                        ClinicDeptFlag = department.ClinicDeptFlag!,
                        UpdateUser = department.UpdateUser!,
                        UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsWard = department.IsWard!,
                        UpdateFlag = department.UpdateFlag!
                    }
                    // Msg = new Msg
                    // {
                    //     Id = "1",
                    //     LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    //     Action = "update",
                    //     PinyinCode = "",
                    //     WubiCode = "",
                    //     InvalidFlag = 1,
                    //     RecordDate = "",
                    //     SuborHospitalDistrict = "A", 
                    //     DeptCode = "1234",
                    //     DeptName = "测试科室",
                    //     OiDeptFlag = "",
                    //     MsDeptFlag = "",
                    //     DeptCategCode = "O",
                    //     DeptCategName = "其它",
                    //     SuperiorDeptCode = "",
                    //     SuperiorDeptName = "",
                    //     Location = "",
                    //     ClinicDeptFlag = "",
                    //     UpdateUser = "",
                    //     UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    //     IsWard = 0,
                    //     UpdateFlag = 0
                    // }
                }
            };
            return GenerateESBXml(entry);
        }

        public string CreateSampleESBXmlry(StaffInfoDetailed staff)
        {
            var entry = new ESBEntry
            {
                AccessControl = new AccessControl
                {
                    SysFlag = 1,
                    UserName = "rsgl",
                    Password = "rsgl",
                    Fid = "PS02005",
                    OrderNo = "PS02005S01001"
                },
                MessageHeader = new MessageHeader
                {
                    OrderNo = "PS02005S01001",
                    Fid = "PS02005",
                    SourceSysCode = "S27",
                    ReturnFlag = -1,
                    HospCode = "GH01",
                    TargetSysCode = "S00",
                    MsgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                },
                MsgInfo = new MsgInfo
                {
                    Flag = "",
                    Msg = new Msg
                    {
                        Id = "1",
                        WubiCode = staff.WubiCode!,
                        UpdateUser = staff.UpdateUser!,
                        UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), // 当前 JST 时间，例如 "2025/05/22 21:41:00"
                        UpdateFlag = staff.UpdateFlag,
                        TitleLevelName = staff.TitleLevelName!,
                        TitleLevelCode = staff.TitleLevelCode!,
                        SuborDeptName = staff.SuborDeptName!,
                        SuborDeptCode = staff.SuborDeptCode!,
                        StaffName = staff.StaffName!,
                        StaffCode = staff.StaffCode!,
                        StaffCategName = staff.StaffCategName!,
                        StaffCategCode = staff.StaffCategCode!,
                        StaffBriefing = staff.StaffBriefing!,
                        RoleAuthorityName = staff.RoleAuthorityName!,
                        RoleAuthorityCode = staff.RoleAuthorityCode!,
                        PoliticalDesc = staff.PoliticalDesc!,
                        PinyinCode = staff.PinyinCode!,
                        PhysiSexName = staff.PhysiSexName!,
                        PhysiSexCode = staff.PhysiSexCode!,
                        PhoneNoOffice = staff.PhoneNoOffice!,
                        PhoneNoHome = staff.PhoneNoHome!,
                        PhoneNo = staff.PhoneNo!,
                        NativeAddr = staff.NativeAddr!,
                        NationName = staff.NationName!,
                        NationCode = staff.NationCode!,
                        MaritalStatusName = staff.MaritalStatusName!,
                        MaritalStatusCode = staff.MaritalStatusCode!,
                        MajorSkillPostName = staff.MajorSkillPostName!,
                        MajorSkillPostCode = staff.MajorSkillPostCode!,
                        LoginPassword = staff.LoginPassword!,
                        LoginName = staff.LoginName!,
                        InvalidFlag = staff.InvalidFlag!,
                        IdNumber = staff.IdNumber!,
                        HukouAddr = staff.HukouAddr!,
                        EthnicName = staff.EthnicName!,
                        EthnicCode = staff.EthnicCode!,
                        Email = staff.Email!,
                        EducationName = staff.EducationName!,
                        EducationCode = staff.EducationCode!,
                        DateBirth = staff.DateBirth!, // 示例 "1990/05/15"
                        CurrAddr = staff.CurrAddr!,
                        CreateUser = staff.CreateUser!,
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    }
                    //     Msg = new Msg
                    //     {
                    //         Id = "1",
                    //         LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    //         Action = "update",
                    //         WubiCode = "IWMTCUA",
                    //         UpdateUser = "006165",
                    //         UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), // 当前 JST 时间，例如 "2025/05/22 21:41:00"
                    //         UpdateFlag = 0,
                    //         TitleLevelName = "主任医师",
                    //         TitleLevelCode = "01",
                    //         SuborDeptName = "消化内科三病区",
                    //         SuborDeptCode = "2983",
                    //         StaffName = "张三",
                    //         StaffCode = "S001",
                    //         StaffCategName = "医生",
                    //         StaffCategCode = "D",
                    //         StaffBriefing = "消化内科专家",
                    //         RoleAuthorityName = "管理员",
                    //         RoleAuthorityCode = "ADMIN",
                    //         PoliticalStatus = "党员",
                    //         PinyinCode = "ZHANGSAN",
                    //         PhysiSexName = "男",
                    //         PhysiSexCode = "M",
                    //         PhoneNoOffice = "010-12345678",
                    //         PhoneNoHome = "010-87654321",
                    //         PhoneNo = "138-1234-5678",
                    //         NativeAddr = "北京市朝阳区",
                    //         NationName = "中国",
                    //         NationCode = "CN",
                    //         MaritalStatusName = "已婚",
                    //         MaritalStatusCode = "MARRIED",
                    //         MajorSkillPostName = "内科医生",
                    //         MajorSkillPostCode = "MD",
                    //         LoginPassword = "hashed_password",
                    //         LoginName = "zhangsan",
                    //         InvalidFlag = 0,
                    //         IdNumber = "11010519900515001X",
                    //         HukouAddr = "北京市海淀区",
                    //         EthnicName = "汉族",
                    //         EthnicCode = "01",
                    //         Email = "zhangsan@example.com",
                    //         EducationName = "博士",
                    //         EducationCode = "PHD",
                    //         DateBirth = "", // 示例 "1990/05/15"
                    //         CurrAddr = "北京市朝阳区望京",
                    //         CreateUser = "006165",
                    //         CreateTime = ""
                    //     }
                }
            };
            return GenerateESBXml(entry);
        }

        public string CreateSampleESBXmlBQ(Departments departments)
        {
            var entry = new ESBEntry
            {
                AccessControl = new AccessControl
                {
                    SysFlag = 1,
                    UserName = "rsgl",
                    Password = "rsgl",
                    Fid = "PS02009",
                    OrderNo = "PS02009S01001"
                },
                MessageHeader = new MessageHeader
                {
                    OrderNo = "PS02009S01001",
                    Fid = "PS02009",
                    SourceSysCode = "S27",
                    ReturnFlag = -1,
                    HospCode = "GH01",
                    TargetSysCode = "S00",
                    MsgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                },
                MsgInfo = new MsgInfo
                {
                    Flag = "",
                    Msg = new Msg
                    {
                        Id = "1",
                        LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Action = "update",
                        WubiCode = departments.WubiCode!,
                        UpdateUser = departments.UpdateUser!,
                        UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), // 当前 JST 时间，例如 "2025/05/22 21:41:00"
                        UpdateFlag = departments.UpdateFlag,
                        // TitleLevelName = "主任医师",
                        //TitleLevelCode = "01",
                        //SuborDeptName = "消化内科三病区",
                        //SuborDeptCode = "2983",
                        DeptName = departments.DeptName!,
                        DeptCode = departments.DeptCode!,
                        //StaffCategName = "医生",
                        //StaffCategCode = "D",
                        //StaffBriefing = "消化内科专家",
                        // RoleAuthorityName = "管理员",
                        // RoleAuthorityCode = "ADMIN",
                        // PoliticalStatus = "党员",
                        PinyinCode = departments.PinyinCode,
                        // PhysiSexName = "男",
                        // PhysiSexCode = "M",
                        // PhoneNoOffice = "010-12345678",
                        // PhoneNoHome = "010-87654321",
                        // PhoneNo = "138-1234-5678",
                        // NativeAddr = "北京市朝阳区",
                        // NationName = "中国",
                        // NationCode = "CN",
                        // MaritalStatusName = "已婚",
                        // MaritalStatusCode = "MARRIED",
                        // MajorSkillPostName = "内科医生",
                        // MajorSkillPostCode = "MD",
                        // LoginPassword = "hashed_password",
                        // LoginName = "zhangsan",
                        InvalidFlag = departments.InvalidFlag!,
                        SuborHospitalDistrict = "A",
                        Location = departments.Location!,
                        RecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    }
                    // Msg = new Msg
                    // {
                    //     Id = "996",
                    //     LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    //     Action = "update",
                    //     WubiCode = "IWMTCUA",
                    //     UpdateUser = "006165",
                    //     // UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), // 当前 JST 时间，例如 "2025/05/22 21:41:00"
                    //     UpdateFlag = 0,
                    //     // TitleLevelName = "主任医师",
                    //     //TitleLevelCode = "01",
                    //     //SuborDeptName = "消化内科三病区",
                    //     //SuborDeptCode = "2983",
                    //     StaffName = "张三",
                    //     StaffCode = "S001",
                    //     //StaffCategName = "医生",
                    //     //StaffCategCode = "D",
                    //     //StaffBriefing = "消化内科专家",
                    //     // RoleAuthorityName = "管理员",
                    //     // RoleAuthorityCode = "ADMIN",
                    //     // PoliticalStatus = "党员",
                    //     // PinyinCode = "ZHANGSAN",
                    //     // PhysiSexName = "男",
                    //     // PhysiSexCode = "M",
                    //     // PhoneNoOffice = "010-12345678",
                    //     // PhoneNoHome = "010-87654321",
                    //     // PhoneNo = "138-1234-5678",
                    //     // NativeAddr = "北京市朝阳区",
                    //     // NationName = "中国",
                    //     // NationCode = "CN",
                    //     // MaritalStatusName = "已婚",
                    //     // MaritalStatusCode = "MARRIED",
                    //     // MajorSkillPostName = "内科医生",
                    //     // MajorSkillPostCode = "MD",
                    //     // LoginPassword = "hashed_password",
                    //     // LoginName = "zhangsan",
                    //     InvalidFlag = 1,
                    //     DeptCategCode = "O",
                    //     DeptCategName = "其它",
                    //     SuperiorDeptCode = "",
                    //     SuperiorDeptName = "",
                    //     Location = "",
                    //     RecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    //     // IdNumber = "11010519900515001X",
                    //     // HukouAddr = "北京市海淀区",
                    //     // EthnicName = "汉族",
                    //     // EthnicCode = "01",
                    //     // Email = "zhangsan@example.com",
                    //     // EducationName = "博士",
                    //     // EducationCode = "PHD",
                    //     // DateBirth = "", // 示例 "1990/05/15"
                    //     // CurrAddr = "北京市朝阳区望京",
                    //     // CreateUser = "006165",
                    //     // CreateTime = ""
                    // }
                }
            };
            return GenerateESBXml(entry);
        }
    }
}