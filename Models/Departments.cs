namespace MQWebApplication.Models;

public class Departments
{
    /// <summary>
    /// 五笔码
    /// </summary>
    public string? WubiCode { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string? UpdateUser { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 更新标志
    /// </summary>
    public string? UpdateFlag { get; set; }

    /// <summary>
    /// 从属院区
    /// </summary>
    public string? SuborHospitalDistrict { get; set; }

    /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? RecordDate { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string? PinyinCode { get; set; }

    /// <summary>
    /// 作废标志
    /// </summary>
    public string? InvalidFlag { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 科室代码
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }
}