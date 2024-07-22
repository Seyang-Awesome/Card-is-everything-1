using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具类，可以存储定时事件，当然也能用DOTWEEN来实现
/// </summary>
public class ScheduleManager : Singleton<ScheduleManager>
{
    protected override bool IsDonDestroyOnLoad => true;
    private List<Schedule> schedules = new();
    
    private void Update()
    {
        if (schedules.Count == 0) return;
        if (schedules[0].endTime <= Time.realtimeSinceStartup)
            InvokeSchedule();
    }

    public void AddSchedule(Schedule schedule)
    {
        if (schedule == null) return;
        schedules.Add(schedule);
        schedules.Sort((schedule1,schedule2) => schedule1.endTime >= schedule2.endTime ? 1 : -1);
        schedule.InvokeStartCallback();
    }

    public void RemoveSchedule(Schedule schedule)
    {
        if (!schedules.Contains(schedule)) return;
        schedules.Remove(schedule);
    }

    private void InvokeSchedule()
    {
        while (schedules.Count != 0 && schedules[0].endTime <= Time.realtimeSinceStartup)
        {
            schedules[0].InvokeEndCallback();
            schedules.RemoveAt(0);
        }
    }
}