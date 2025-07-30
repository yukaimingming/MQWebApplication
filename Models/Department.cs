namespace MQWebApplication.Models;

/// <summary>
/// 科室信息实体类
/// </summary>
public class Department
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
    public string? UpdateTime { get; set; }

    /// <summary>
    /// 更新标志
    /// </summary>
    public string? UpdateFlag { get; set; }

    /// <summary>
    /// 上级科室名称
    /// </summary>
    public string? SuperiorDeptName { get; set; }

    /// <summary>
    /// 上级科室代码
    /// </summary>
    public string? SuperiorDeptCode { get; set; }

    /// <summary>
    /// 从属院区
    /// </summary>
    public string? SuborHospitalDistrict { get; set; }

    /// <summary>
    /// 录入日期
    /// </summary>
    public string? RecordDate { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string? PinyinCode { get; set; }

    /// <summary>
    /// 门诊住院科室标志
    /// </summary>
    public string? OiDeptFlag { get; set; }

    /// <summary>
    /// 内外科标志
    /// </summary>
    public string? MsDeptFlag { get; set; }

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
    /// 科室类别名称
    /// </summary>
    public string? DeptCategName { get; set; }

    /// <summary>
    /// 科室类别代码
    /// </summary>
    public string? DeptCategCode { get; set; }

    /// <summary>
    /// 临床科室标志
    /// </summary>
    public string? ClinicDeptFlag { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 是否病区
    /// </summary>
    public string? IsWard { get; set; }
}