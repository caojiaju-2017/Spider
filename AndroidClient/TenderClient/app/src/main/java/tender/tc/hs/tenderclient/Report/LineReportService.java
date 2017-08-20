package tender.tc.hs.tenderclient.Report;

import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Handler;
import android.view.View;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Objects;

import lecho.lib.hellocharts.gesture.ContainerScrollType;
import lecho.lib.hellocharts.gesture.ZoomType;
import lecho.lib.hellocharts.model.Axis;
import lecho.lib.hellocharts.model.AxisValue;
import lecho.lib.hellocharts.model.Line;
import lecho.lib.hellocharts.model.LineChartData;
import lecho.lib.hellocharts.model.PieChartData;
import lecho.lib.hellocharts.model.PointValue;
import lecho.lib.hellocharts.model.SliceValue;
import lecho.lib.hellocharts.model.ValueShape;
import lecho.lib.hellocharts.model.Viewport;
import lecho.lib.hellocharts.util.ChartUtils;
import lecho.lib.hellocharts.view.LineChartView;
import lecho.lib.hellocharts.view.PieChartView;
import tender.tc.hs.tenderclient.HsApplication;
import tender.tc.hs.tenderclient.R;

/**
 * Created by caojiaju on 2016/8/19.
 */
public class LineReportService {

    private LineChartView lineChart;
    private List<PointValue> mPointValues = new ArrayList<PointValue>();
    private List<AxisValue> mAxisXValues = new ArrayList<AxisValue>();

    public LineReportService(LineChartView lcv)
    {
        lineChart = lcv;
    }
    /**
     * 设置X 轴的显示
     */
    public void getAxisXLables(ReportObject rp){
//        int index = 0;
//        for (Map.Entry<String, Object> entry : rp.collectionDatas.entrySet()) {
//            mAxisXValues.add(new AxisValue(index++).setLabel(entry.getKey()));
//        }

        for (int index = 0 ; index < rp._keyLists.size(); index++) {
            mAxisXValues.add(new AxisValue(index++).setLabel(rp._keyLists.get(index)));
        }
    }
    /**
     * 图表的每个点的显示
     */
    public void getAxisPoints(ReportObject rp) {

//        int index = 0;
//        for (Map.Entry<String, Object> entry : rp.collectionDatas.entrySet()) {
//            mPointValues.add(new PointValue(index++, Integer.parseInt(entry.getValue().toString())));
//        }

        for (int index = 0 ; index < rp._valueLists.size(); index ++) {
            mPointValues.add(new PointValue(index++, rp._valueLists.get(index)));
        }
    }

    public void initLineChart(ReportObject rb){
            Line line = new Line(mPointValues).setColor(Color.parseColor("#FFCD41"));  //折线的颜色（橙色）
            List<Line> lines = new ArrayList<Line>();
            line.setShape(ValueShape.CIRCLE);//折线图上每个数据点的形状  这里是圆形 （有三种 ：ValueShape.SQUARE  ValueShape.CIRCLE  ValueShape.DIAMOND）
            line.setCubic(false);//曲线是否平滑，即是曲线还是折线
            line.setFilled(false);//是否填充曲线的面积
            line.setHasLabels(true);//曲线的数据坐标是否加上备注
//      line.setHasLabelsOnlyForSelected(true);//点击数据坐标提示数据（设置了这个line.setHasLabels(true);就无效）
            line.setHasLines(true);//是否用线显示。如果为false 则没有曲线只有点显示
            line.setHasPoints(true);//是否显示圆点 如果为false 则没有原点只有点显示（每个数据点都是个大的圆点）
            lines.add(line);
            LineChartData data = new LineChartData();
            data.setLines(lines);

            //坐标轴
            Axis axisX = new Axis(); //X轴
            axisX.setHasTiltedLabels(true);  //X坐标轴字体是斜的显示还是直的，true是斜的显示
            axisX.setTextColor(Color.parseColor("#666666"));  //设置字体颜色
            axisX.setName(rb.xLabelName);  //表格名称
            axisX.setTextSize(11);//设置字体大小
            axisX.setMaxLabelChars(8); //最多几个X轴坐标，意思就是你的缩放让X轴上数据的个数7<=x<=mAxisXValues.length
            axisX.setValues(mAxisXValues);  //填充X轴的坐标名称
            data.setAxisXBottom(axisX); //x 轴在底部
            //data.setAxisXTop(axisX);  //x 轴在顶部
            axisX.setHasLines(true); //x 轴分割线

            // Y轴是根据数据的大小自动设置Y轴上限(在下面我会给出固定Y轴数据个数的解决方案)
            Axis axisY = new Axis();  //Y轴
            axisY.setName(rb.yLabelName);//y轴标注
            axisY.setTextColor(Color.parseColor("#666666"));  //设置字体颜色
            axisY.setTextSize(11);//设置字体大小
            data.setAxisYLeft(axisY);  //Y轴设置在左边
            //data.setAxisYRight(axisY);  //y轴设置在右边


            //设置行为属性，支持缩放、滑动以及平移
            lineChart.setInteractive(true);
            lineChart.setZoomType(ZoomType.HORIZONTAL);
            lineChart.setMaxZoom((float) 2);//最大方法比例
            lineChart.setContainerScrollEnabled(true, ContainerScrollType.HORIZONTAL);
            lineChart.setLineChartData(data);
            lineChart.setVisibility(View.VISIBLE);
            /**注：下面的7，10只是代表一个数字去类比而已
             * 当时是为了解决X轴固定数据个数。见（http://forum.xda-developers.com/tools/programming/library-hellocharts-charting-library-t2904456/page2）;
             */
            Viewport v = new Viewport(lineChart.getMaximumViewport());
            v.left = 0;
            v.right= 7;
            lineChart.setCurrentViewport(v);
        }

}