using UnityEngine;
using TMPro;

public class CalendarUI : MonoBehaviour
{
    public TextMeshProUGUI monthText;
    public Transform gridArea;
    public CalendarCell cellPrefab;

    private int currentYear;
    private int currentMonth;
    private CalendarCell[] cells;

    void Start()
    {
        System.DateTime now = System.DateTime.Now;
        currentYear = now.Year;
        currentMonth = now.Month;

        GenerateCells();
        UpdateCalendar();
    }

    void GenerateCells()
    {
        cells = new CalendarCell[42];

        for (int i = 0; i < 42; i++)
        {
            cells[i] = Instantiate(cellPrefab, gridArea);
        }
    }

    public void UpdateCalendar()
    {
        System.DateTime firstDay = new System.DateTime(currentYear, currentMonth, 1);
        int startDayIndex = (int)firstDay.DayOfWeek;

        int daysInMonth = System.DateTime.DaysInMonth(currentYear, currentMonth);

        monthText.text = $"{currentYear}년 {currentMonth}월";

        int day = 1;

        for (int i = 0; i < 42; i++)
        {
            if (i < startDayIndex || day > daysInMonth)
            {
                cells[i].SetEmpty();
            }
            else
            {
                string dateString = new System.DateTime(currentYear, currentMonth, day).ToString("yyyy-MM-dd");
                cells[i].SetDate(day, dateString);
                day++;
            }
        }
    }

    public void NextMonth()
    {
        currentMonth++;
        if (currentMonth > 12)
        {
            currentMonth = 1;
            currentYear++;
        }
        UpdateCalendar();
    }

    public void PrevMonth()
    {
        currentMonth--;
        if (currentMonth < 1)
        {
            currentMonth = 12;
            currentYear--;
        }
        UpdateCalendar();
    }
}
