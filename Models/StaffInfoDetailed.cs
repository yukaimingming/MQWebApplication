namespace MQWebApplication.Models;

/// <summary>
/// 代表工作人员详细信息的实体类，用于存储和操作工作人员数据。
/// </summary>
public class StaffInfoDetailed
{
    /// <summary>
    /// 五笔编码，用于标识工作人员的五笔输入法代码。
    /// </summary>
    public string? WubiCode { get; set; }

    /// <summary>
    /// 修改人工号，表示最后修改该记录的工作人员编号。
    /// </summary>
    public string? UpdateUser { get; set; }

    /// <summary>
    /// 修改人时间，表示最后修改该记录的时间，推荐格式为 yyyy/MM/dd HH:mm:ss (JST)。
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 更新标志，指示记录是否已更新（0 表示未更新，1 表示已更新）。
    /// </summary>
    public string? UpdateFlag { get; set; }

    /// <summary>
    /// 职称级别名称，描述工作人员的职称名称（如“主任医师”）。
    /// </summary>
    public string? TitleLevelName { get; set; }

    /// <summary>
    /// 职称级别代码，职称级别的编码（如“01”）。
    /// </summary>
    public string? TitleLevelCode { get; set; }

    /// <summary>
    /// 从属科室名称，工作人员所属科室的名称（如“消化内科三病区”）。
    /// </summary>
    public string? SuborDeptName { get; set; }

    /// <summary>
    /// 从属科室代码，科室的唯一标识代码（如“2983”）。
    /// </summary>
    public string? SuborDeptCode { get; set; }

    /// <summary>
    /// 职工姓名，工作人员的真实姓名。
    /// </summary>
    public string? StaffName { get; set; }

    /// <summary>
    /// 职工工号，工作人员的唯一工号（如“S001”）。
    /// </summary>
    public string? StaffCode { get; set; }

    /// <summary>
    /// 职工类别名称，描述工作人员的类别（如“医生”）。
    /// </summary>
    public string? StaffCategName { get; set; }

    /// <summary>
    /// 职工类别代码，职工类别的编码（如“D”）。
    /// </summary>
    public string? StaffCategCode { get; set; }

    /// <summary>
    /// 职工简介，工作人员的简要介绍或备注信息。
    /// </summary>
    public string? StaffBriefing { get; set; }

    /// <summary>
    /// 角色权限名称，描述工作人员的角色权限名称（如“管理员”）。
    /// </summary>
    public string? RoleAuthorityName { get; set; }

    /// <summary>
    /// 角色权限代码，角色权限的唯一标识代码（如“ADMIN”）。
    /// </summary>
    public string? RoleAuthorityCode { get; set; }

    /// <summary>
    /// 政治面貌，工作人员的政治身份（如“党员”）。
    /// </summary>
    public string? PoliticalDesc { get; set; }

    /// <summary>
    /// 拼音码，工作人员姓名的拼音代码（如“ZHANGSAN”）。
    /// </summary>
    public string? PinyinCode { get; set; }

    /// <summary>
    /// 生理性别名称，工作人员的性别名称（如“男”）。
    /// </summary>
    public string? PhysiSexName { get; set; }

    /// <summary>
    /// 生理性别代码，性别的编码（如“M”）。
    /// </summary>
    public string? PhysiSexCode { get; set; }

    /// <summary>
    /// 办公室电话，工作人员的办公电话号码。
    /// </summary>
    public string? PhoneNoOffice { get; set; }

    /// <summary>
    /// 住址电话，工作人员的家庭电话号码。
    /// </summary>
    public string? PhoneNoHome { get; set; }

    /// <summary>
    /// 联系电话，工作人员的移动电话号码。
    /// </summary>
    public string? PhoneNo { get; set; }

    /// <summary>
    /// 籍贯地址，工作人员的出生地或籍贯地址。
    /// </summary>
    public string? NativeAddr { get; set; }

    /// <summary>
    /// 国籍名称，工作人员的国籍（如“中国”）。
    /// </summary>
    public string? NationName { get; set; }

    /// <summary>
    /// 国籍代码，国籍的编码（如“CN”）。
    /// </summary>
    public string? NationCode { get; set; }

    /// <summary>
    /// 婚姻状况名称，工作人员的婚姻状态名称（如“已婚”）。
    /// </summary>
    public string? MaritalStatusName { get; set; }

    /// <summary>
    /// 婚姻状况代码，婚姻状态的编码（如“MARRIED”）。
    /// </summary>
    public string? MaritalStatusCode { get; set; }

    /// <summary>
    /// 专业技术职务名称，工作人员的专业技术职务名称（如“内科医生”）。
    /// </summary>
    public string? MajorSkillPostName { get; set; }

    /// <summary>
    /// 专业技术职务代码，职务的编码（如“MD”）。
    /// </summary>
    public string? MajorSkillPostCode { get; set; }

    /// <summary>
    /// 登录密码，工作人员的登录系统密码（应加密存储）。
    /// </summary>
    public string? LoginPassword { get; set; }

    /// <summary>
    /// 登录名，工作人员的登录系统用户名。
    /// </summary>
    public string? LoginName { get; set; }

    /// <summary>
    /// 作废标志，指示记录是否作废（0 表示有效，1 表示作废）。
    /// </summary>
    public string? InvalidFlag { get; set; }

    /// <summary>
    /// 身份证号码，工作人员的身份证号码。
    /// </summary>
    public string? IdNumber { get; set; }

    /// <summary>
    /// 户口地址，工作人员的户籍地址。
    /// </summary>
    public string? HukouAddr { get; set; }

    /// <summary>
    /// 民族名称，工作人员的民族名称（如“汉族”）。
    /// </summary>
    public string? EthnicName { get; set; }

    /// <summary>
    /// 民族代码，民族的编码（如“01”）。
    /// </summary>
    public string? EthnicCode { get; set; }

    /// <summary>
    /// 电子邮件，工作人员的电子邮件地址。
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 学历名称，工作人员的学历名称（如“博士”）。
    /// </summary>
    public string? EducationName { get; set; }

    /// <summary>
    /// 学历代码，学历的编码（如“PHD”）。
    /// </summary>
    public string? EducationCode { get; set; }

    /// <summary>
    /// 出生日期，工作人员的出生日期，推荐格式为 yyyy/MM/dd。
    /// </summary>
    public DateTime? DateBirth { get; set; }

    /// <summary>
    /// 现住地址，工作人员当前的居住地址。
    /// </summary>
    public string? CurrAddr { get; set; }

    /// <summary>
    /// 创建人工号，创建该记录的工作人员编号。
    /// </summary>
    public string? CreateUser { get; set; }

    /// <summary>
    /// 创建时间，记录创建的时间，推荐格式为 yyyy/MM/dd HH:mm:ss (JST)。
    /// </summary>
    public DateTime? CreateTime { get; set; }
}