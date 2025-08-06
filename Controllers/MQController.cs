using Microsoft.AspNetCore.Mvc;
using MQWebApplication.DBhelpler;
using MQWebApplication.Models;
using Newtonsoft.Json;
using System.Data;
using MQSDK;
using MQSDK.Models;


namespace MQWebApplication.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MQController : ControllerBase
{
    #region Fields

    private readonly IConfiguration? _configuration;
    private readonly ILogger<MQController> _logger;
    private readonly IQueryHelperSqlserver _sqlserver;
    private readonly IGetxml _getxml;

    #endregion Fields

    #region Constructors

    public MQController(ILogger<MQController> logger, IQueryHelperSqlserver query, IConfiguration configuration,
        IGetxml getxml)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sqlserver = query ?? throw new ArgumentNullException(nameof(query));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _getxml = getxml ?? throw new ArgumentNullException(nameof(getxml));
    }

    #endregion Constructors

    #region Methods

    [EndpointDescription("发送科室MQ消息")]
    [HttpPost]
    public IActionResult ComposePutAndGetMsgks([FromBody] PutMsgDto data)
    {
        // _logger.LogInformation("发送MQ消息:{0}", $"{JsonConvert.SerializeObject(data)}");
#if DEBUG
        data.QMgrName = "QMGR.P27";
#endif
        data.DataChannel = "PS02004";
        data.WaitInternal = 10000;
        var ds = _sqlserver.QueryDataSetSql(_configuration!["deptsql"]!);

        //如果ds没有数据就返回
        if (ds.Tables.Count <= 0 && ds.Tables[1].Rows.Count <= 0) return Ok(new { code = "-1", msg = "无科室数据" });
        _logger.LogInformation("查询的科室数据查询" + ds.Tables.Count.ToString());
        // 转换为 List<Department>
        //var query = null as List<Department>;
        var query = (from row in ds.Tables[0]!.AsEnumerable()
                     select new Department //  
                     {
                         WubiCode = row.Field<string>("WUBI_CODE"),
                         UpdateUser = row.Field<string>("UPDATE_USER"),
                         UpdateTime = row.Field<string>("UPDATE_TIME"),
                         UpdateFlag = row.Field<string>("UPDATE_FLAG"),
                         SuperiorDeptName = row.Field<string>("SUPERIOR_DEPT_NAME"),
                         SuperiorDeptCode = row.Field<string>("SUPERIOR_DEPT_CODE"),
                         SuborHospitalDistrict = row.Field<string>("SUBOR_HOSPITAL_DISTRICT"),
                         RecordDate = row.Field<string>("RECORD_DATE"),
                         PinyinCode = row.Field<string>("PINYIN_CODE"),
                         OiDeptFlag = row.Field<string>("OI_DEPT_FLAG"),
                         MsDeptFlag = row.Field<string>("MS_DEPT_FLAG"),
                         DeptCategCode = row.Field<string>("DEPT_CATEG_CODE"),
                         DeptCategName = row.Field<string>("DEPT_CATEG_NAME"),
                         Location = row.Field<string>("LOCATION"),
                         ClinicDeptFlag = row.Field<string>("CLINIC_DEPT_FLAG"),
                         DeptCode = row.Field<string>("DEPT_CODE"),
                         DeptName = row.Field<string>("DEPT_NAME"),
                         InvalidFlag = row.Field<string>("INVALID_FLAG"),
                         IsWard = row.Field<string>("IS_WARD")
                     }).ToList();
        _logger.LogInformation("查询的科室LIST数据" + query.Count.ToString());
        var resutls = new List<SdkResponse<GetMsgResult>>();
        foreach (var value in query)
        {
            var department = new Department
            {
                WubiCode = value.WubiCode,
                UpdateUser = value.UpdateUser,
                UpdateTime = value.UpdateTime,
                UpdateFlag = value.UpdateFlag,
                SuperiorDeptName = value.SuperiorDeptName,
                SuperiorDeptCode = value.SuperiorDeptCode,
                SuborHospitalDistrict = value.SuborHospitalDistrict,
                RecordDate = value.RecordDate,
                PinyinCode = value.PinyinCode,
                OiDeptFlag = value.OiDeptFlag,
                MsDeptFlag = value.MsDeptFlag,
                DeptCategCode = value.DeptCategCode,
                DeptCategName = value.DeptCategName,
                Location = value.Location,
                ClinicDeptFlag = value.ClinicDeptFlag,
                DeptCode = value.DeptCode,
                DeptName = value.DeptName,
                InvalidFlag = value.InvalidFlag,
                IsWard = value.IsWard
            };

            //data.Msg = JsonConvert.SerializeObject(department);
            _logger.LogInformation("生成json:{@department}", $"{JsonConvert.SerializeObject(department)}");
            // Department department = new();

            using (var mqsdk = SdkClient.Create())
            {
                data.QMgrName = _configuration?["QMgrName"];
                //var msgid = mqsdk.PutMsg(data.Msg, data.DataChannel);
                data.Msg = _getxml.CreateSampleESBXmljg(department);
                //data.Msg = "123";
                _logger.LogInformation("生成科室XML消息体内容:{@data}", data.Msg);
                _logger.LogInformation("生成科室MQ消息体内容:{@Data}", data);
                var result = mqsdk.ComposePutAndGetMsg(
                            data.QMgrName,
                            data.DataChannel,
                            data.WaitInternal,
                            data.Msg
                        );


                _logger.LogInformation("接收返回MQ消息:{@result}", result);
                resutls.Add(result);
                //return Ok(result);
            }
        }

        return Ok(resutls);
    }

    [EndpointDescription("发送病区MQ消息")]
    [HttpPost]
    public IActionResult ComposePutAndGetMsgbq([FromBody] PutMsgDto data)
    {
        // _logger.LogInformation("发送MQ消息:{0}", $"{JsonConvert.SerializeObject(data)}");
#if DEBUG
        data.QMgrName = "QMGR.P27";
#endif
        data.DataChannel = "PS02009";
        data.WaitInternal = 10000;
        var ds = _sqlserver.QueryDataSetSql(_configuration!["bqsql"]!);
        //如果ds没有数据就返回
        if (ds.Tables.Count <= 0 && ds.Tables[1].Rows.Count <= 0) return Ok(new { code = "-1", msg = "无科室数据" });
        _logger.LogInformation("查询的病区数据查询" + ds.Tables.Count.ToString());
        // 转换为 List<Department>
        //var query = null as List<Department>;
        var query = (from row in ds.Tables[0]!.AsEnumerable()
                     select new Departments //  
                     {
                         WubiCode = row.Field<string>("WUBI_CODE"),
                         UpdateUser = row.Field<string>("UPDATE_USER"),
                         UpdateTime = row.Field<string>("UPDATE_TIME"),
                         UpdateFlag = row.Field<string>("UPDATE_FLAG"),
                         //             SuperiorDeptName = row.Field<string>("superiordeptname"),
                         //             SuperiorDeptCode = row.Field<string>("superiordeptcode"),
                         SuborHospitalDistrict = row.Field<string>("SUBOR_HOSPITAL_DISTRICT"),
                         RecordDate = row.Field<string>("RECORD_DATE"),
                         PinyinCode = row.Field<string>("PINYIN_CODE"),
                         //             OiDeptFlag = row.Field<string>("oideptflag"),
                         //             MsDeptFlag = row.Field<string>("msdeptflag"),
                         //             DeptCategCode = row.Field<string>("deptcategcode"),
                         //             DeptCategName = row.Field<string>("deptcategname"),
                         Location = row.Field<string>("LOCATION"),
                         //             ClinicDeptFlag = row.Field<string>("clinicdeptflag"),
                         DeptCode = row.Field<string>("DEPT_CODE"),
                         DeptName = row.Field<string>("DEPT_NAME"),
                         InvalidFlag = row.Field<string>("INVALID_FLAG")
                         //             IsWard = row.Field<string>("isward")
                     }).ToList();
        _logger.LogInformation("查询的病区LIST数据" + query.Count.ToString());
        var resutls = new List<SdkResponse<GetMsgResult>>();
        foreach (var value in query)
        {
            var department = new Departments
            {
                WubiCode = value.WubiCode,
                UpdateUser = value.UpdateUser,
                UpdateTime = value.UpdateTime,
                UpdateFlag = value.UpdateFlag,
                //         SuperiorDeptName = value.SuperiorDeptName,
                //         SuperiorDeptCode = value.SuperiorDeptCode,
                SuborHospitalDistrict = value.SuborHospitalDistrict,
                RecordDate = value.RecordDate,
                PinyinCode = value.PinyinCode,
                //         OiDeptFlag = value.OiDeptFlag,
                //         MsDeptFlag = value.MsDeptFlag,
                //         DeptCategCode = value.DeptCategCode,
                //         DeptCategName = value.DeptCategName,
                Location = value.Location,
                //         ClinicDeptFlag = value.ClinicDeptFlag,
                DeptCode = value.DeptCode,
                DeptName = value.DeptName,
                InvalidFlag = value.InvalidFlag
                //         IsWard = value.IsWard
            };

            //data.Msg = JsonConvert.SerializeObject(department);
            _logger.LogInformation("生成json:{@department}", $"{JsonConvert.SerializeObject(department)}");
            // Department department = new();
            using (var mqsdk = SdkClient.Create())
            {
                data.QMgrName = _configuration?["QMgrName"];
                //var msgid = mqsdk.PutMsg(data.Msg, data.DataChannel);
                data.Msg = _getxml.CreateSampleESBXmlBQ(department);
                //data.Msg = "123";
                _logger.LogInformation("生成病区XML消息体内容:{@data}", data.Msg);
                _logger.LogInformation("生成病区MQ消息体内容:{@Data}", data);
                var result = mqsdk.ComposePutAndGetMsg(data.QMgrName, data.DataChannel, data.WaitInternal,
                    data.Msg);

                _logger.LogInformation("接收返回MQ消息:{@result}", result);
                resutls.Add(result);
                //return Ok(result);
            }
        }

        return Ok(resutls);
    }

    [EndpointDescription("发送人员MQ消息")]
    [HttpPost]
    public IActionResult ComposePutAndGetMsgry([FromBody] PutMsgDto data)
    {
        // _logger.LogInformation("发送MQ消息:{0}", $"{JsonConvert.SerializeObject(data)}");
#if DEBUG
        data.QMgrName = "QMGR.P27";
#endif
        data.DataChannel = "PS02005";
        data.WaitInternal = 10000;
        var ds = _sqlserver.QueryDataSetSql(_configuration!["rysql"]!);
        //如果ds没有数据就返回
        if (ds.Tables.Count <= 0 && ds.Tables[1].Rows.Count <= 0) return Ok(new { code = "-1", msg = "无人员数据" });
        _logger.LogInformation("查询的人员数据" + ds.Tables.Count.ToString());
        // 转换为 List<Department>
        // var query = null as List<StaffInfoDetailed>;
        var query = (from row in ds.Tables[0]!.AsEnumerable()
                     select new StaffInfoDetailed //  
                     {
                         WubiCode = row.Field<string>("WUBI_CODE"),
                         UpdateUser = row.Field<string>("UPDATE_USER"),
                         UpdateTime = row.Field<string>("UPDATE_TIME"),
                         UpdateFlag = row.Field<string>("UPDATE_FLAG"),
                         TitleLevelName = row.Field<string>("TITLE_LEVEL_NAME"),
                         TitleLevelCode = row.Field<string>("TITLE_LEVEL_CODE"),
                         SuborDeptName = row.Field<string>("SUBOR_DEPT_NAME"),
                         SuborDeptCode = row.Field<string>("SUBOR_DEPT_CODE"),
                         StaffName = row.Field<string>("STAFF_NAME"),
                         StaffCode = row.Field<string>("STAFF_CODE"),
                         StaffCategName = row.Field<string>("STAFF_CATEG_NAME"),
                         StaffCategCode = row.Field<string>("STAFF_CATEG_CODE"),
                         StaffBriefing = row.Field<string>("STAFF_BRIEFING"),
                         RoleAuthorityName = row.Field<string>("ROLE_AUTHORITY_NAME"),
                         RoleAuthorityCode = row.Field<string>("ROLE_AUTHORITY_CODE"),
                         PoliticalDesc = row.Field<string>("POLITICAL_DESC"),
                         PinyinCode = row.Field<string>("PINYIN_CODE"),
                         PhysiSexName = row.Field<string>("PHYSI_SEX_NAME"),
                         PhysiSexCode = row.Field<string>("PHYSI_SEX_CODE"),
                         PhoneNoOffice = row.Field<string>("PHONE_NO_OFFICE"),
                         PhoneNoHome = row.Field<string>("PHONE_NO_HOME"),
                         PhoneNo = row.Field<string>("PHONE_NO"),
                         NativeAddr = row.Field<string>("NATIVE_ADDR"),
                         NationName = row.Field<string>("NATION_NAME"),
                         NationCode = row.Field<string>("NATION_CODE"),
                         MaritalStatusName = row.Field<string>("MARITAL_STATUS_NAME"),
                         MaritalStatusCode = row.Field<string>("MARITAL_STATUS_CODE"),
                         MajorSkillPostName = row.Field<string>("MAJOR_SKILL_POST_NAME"),
                         MajorSkillPostCode = row.Field<string>("MAJOR_SKILL_POST_CODE"),
                         LoginPassword = row.Field<string>("LOGIN_PASSWORD"),
                         LoginName = row.Field<string>("LOGIN_NAME"),
                         InvalidFlag = row.Field<string>("INVALID_FLAG"),
                         IdNumber = row.Field<string>("ID_NUMBER"),
                         HukouAddr = row.Field<string>("HUKOU_ADDR"),
                         EthnicName = row.Field<string>("ETHNIC_NAME"),
                         EthnicCode = row.Field<string>("ETHNIC_CODE"),
                         Email = row.Field<string>("EMAIL"),
                         EducationName = row.Field<string>("EDUCATION_NAME"),
                         EducationCode = row.Field<string>("EDUCATION_CODE"),
                         DateBirth = row.Field<string?>("DATE_BIRTH"),
                         CurrAddr = row.Field<string>("CURR_ADDR"),
                         CreateUser = row.Field<string>("CREATE_USER"),
                         CreateTime = row.Field<string>("CREATE_TIME"),
                         yb_code = row.Field<string>("YB_CODE")
                     }).ToList();
        _logger.LogInformation("查询的人员LIST数据" + query.Count.ToString());
        var resutls = new List<SdkResponse<GetMsgResult>>();
        foreach (var value in query)
        {
            var staffInfo = new StaffInfoDetailed
            {
                WubiCode = value.WubiCode,
                UpdateUser = value.UpdateUser,
                UpdateTime = value.UpdateTime,
                UpdateFlag = value.UpdateFlag,
                TitleLevelName = value.TitleLevelName,
                TitleLevelCode = value.TitleLevelCode,
                SuborDeptName = value.SuborDeptName,
                SuborDeptCode = value.SuborDeptCode,
                StaffName = value.StaffName,
                StaffCode = value.StaffCode,
                StaffCategName = value.StaffCategName,
                StaffCategCode = value.StaffCategCode,
                StaffBriefing = value.StaffBriefing,
                RoleAuthorityName = value.RoleAuthorityName,
                RoleAuthorityCode = value.RoleAuthorityCode,
                PoliticalDesc = value.PoliticalDesc,
                PinyinCode = value.PinyinCode,
                PhysiSexName = value.PhysiSexName,
                PhysiSexCode = value.PhysiSexCode,
                PhoneNoOffice = value.PhoneNoOffice,
                PhoneNoHome = value.PhoneNoHome,
                PhoneNo = value.PhoneNo,
                NativeAddr = value.NativeAddr,
                NationName = value.NationName,
                NationCode = value.NationCode,
                MaritalStatusName = value.MaritalStatusName,
                MaritalStatusCode = value.MaritalStatusCode,
                MajorSkillPostName = value.MajorSkillPostName,
                MajorSkillPostCode = value.MajorSkillPostCode,
                LoginPassword = value.LoginPassword,
                LoginName = value.LoginName,
                InvalidFlag = value.InvalidFlag,
                IdNumber = value.IdNumber,
                HukouAddr = value.HukouAddr,
                EthnicName = value.EthnicName,
                EthnicCode = value.EthnicCode,
                Email = value.Email,
                EducationName = value.EducationName,
                EducationCode = value.EducationCode,
                DateBirth = value.DateBirth,
                CurrAddr = value.CurrAddr,
                CreateUser = value.CreateUser,
                CreateTime = value.CreateTime
            };
            //data.Msg = JsonConvert.SerializeObject(department);
            _logger.LogInformation("生成json:{0}", $"{JsonConvert.SerializeObject(staffInfo)}");
            //StaffInfoDetailed staff = new();
            using (var mqsdk = SdkClient.Create())
            {
                data.QMgrName = _configuration?["QMgrName"];
                //var msgid = mqsdk.PutMsg(data.Msg, data.DataChannel);
                data.Msg = _getxml.CreateSampleESBXmlry(staffInfo);
                //data.Msg = "123";
                _logger.LogInformation("生成员工XML消息体内容:{@data}", data.Msg);
                _logger.LogInformation("生成员工MQ消息体内容:{@Data}", data);
                var result = mqsdk.ComposePutAndGetMsg(data.QMgrName, data.DataChannel, data.WaitInternal,
                    data.Msg);

                _logger.LogInformation("接收返回MQ消息:{@result}", result);
                resutls.Add(result);
                //return Ok(result);
            }
        }

        return Ok(resutls);
    }
}

#endregion Methods