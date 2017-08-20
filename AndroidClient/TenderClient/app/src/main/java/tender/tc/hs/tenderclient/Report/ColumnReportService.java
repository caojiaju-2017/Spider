package tender.tc.hs.tenderclient.Report;

import android.graphics.Color;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Random;

import lecho.lib.hellocharts.formatter.ColumnChartValueFormatter;
import lecho.lib.hellocharts.formatter.SimpleColumnChartValueFormatter;
import lecho.lib.hellocharts.model.Axis;
import lecho.lib.hellocharts.model.AxisValue;
import lecho.lib.hellocharts.model.Column;
import lecho.lib.hellocharts.model.ColumnChartData;
import lecho.lib.hellocharts.model.PieChartData;
import lecho.lib.hellocharts.model.SliceValue;
import lecho.lib.hellocharts.model.SubcolumnValue;
import lecho.lib.hellocharts.util.ChartUtils;
import lecho.lib.hellocharts.view.ColumnChartView;
import lecho.lib.hellocharts.view.PieChartView;

/**
 * Created by caojiaju on 2016/8/19.
 */
public class ColumnReportService {

    private ColumnChartView pieChart;
    List<Column> columns = new ArrayList<Column>();
    List<AxisValue> axisValues = new ArrayList<AxisValue>();
    List<SubcolumnValue> values= new ArrayList<>();
    ColumnChartData data ;

    public ColumnReportService(ColumnChartView pcv)
    {
        pieChart = pcv;
    }

    public void setData(ReportObject ro)
    {
        // 使用的 7列，每列1个subcolumn。
        int numSubcolumns = 1;
        int numColumns = ro._keyLists.size();

        //遍历列数numColumns
//        int index = 0;
        for (int index = 0 ; index < ro._valueLists.size(); index ++) {

            values = new ArrayList<SubcolumnValue>();
            //遍历每一列的每一个子列
            for (int j = 0; j < numSubcolumns; ++j) {
                //为每一柱图添加颜色和数值
                int f = ro._valueLists.get(index);
                values.add(new SubcolumnValue(f, ChartUtils.pickColor()));
            }
            //创建Column对象
            Column column = new Column(values);
            //这一步是能让圆柱标注数据显示带小数的重要一步 让我找了好久问题
            //作者回答https://github.com/lecho/hellocharts-android/issues/185
            ColumnChartValueFormatter chartValueFormatter = new SimpleColumnChartValueFormatter(2);
            column.setFormatter(chartValueFormatter);
            //是否有数据标注
            column.setHasLabels(true);
            //是否是点击圆柱才显示数据标注
            column.setHasLabelsOnlyForSelected(false);
            columns.add(column);
            //给x轴坐标设置描述
            axisValues.add(new AxisValue(index).setLabel(ro._keyLists.get(index)));
        }
    }
    public void initChart(ReportObject ro) {

        //创建一个带有之前圆柱对象column集合的ColumnChartData
        data= new ColumnChartData(columns);

        //定义x轴y轴相应参数
        Axis axisX = new Axis();
        Axis axisY = new Axis().setHasLines(true);
        axisY.setName(ro.yLabelName);//轴名称
        axisY.hasLines();//是否显示网格线
        axisY.setTextColor(Color.BLACK);//颜色

        axisX.hasLines();
        axisX.setTextColor(Color.BLACK);
        axisX.setName(ro.xLabelName);//轴名称
        axisX.setTextColor(Color.parseColor("#666666"));  //设置字体颜色
        axisX.setValues(axisValues);
        axisX.setMaxLabelChars(8);
        axisX.setHasLines(true); //x 轴分割线
        //把X轴Y轴数据设置到ColumnChartData 对象中
        data.setAxisXBottom(axisX);
        data.setAxisYLeft(axisY);

        //给表填充数据，显示出来
        pieChart.setColumnChartData(data);
    }

}
