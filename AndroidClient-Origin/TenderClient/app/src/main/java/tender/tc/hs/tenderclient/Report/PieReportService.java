package tender.tc.hs.tenderclient.Report;

import android.graphics.Color;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import lecho.lib.hellocharts.model.PieChartData;
import lecho.lib.hellocharts.model.SliceValue;
import lecho.lib.hellocharts.util.ChartUtils;
import lecho.lib.hellocharts.view.PieChartView;

/**
 * Created by caojiaju on 2016/8/19.
 */
public class PieReportService {

    private PieChartView pieChart;
    private PieChartData pieChardata;
    List<SliceValue> values = new ArrayList<SliceValue>();
//    private int[] data = {21,20,9,2,8,33,14,12};

    public  PieReportService(PieChartView pcv)
    {
        pieChart = pcv;
    }
    /**
     * 获取数据
     */
    public void setPieChartData(ReportObject ro){

//        int index = 0;
//        for (int i = 0; i < ro.collectionDatas.size(); ++i) {
//            SliceValue sliceValue = new SliceValue((float) data[i], Color.parseColor(getRandColorCode()));//这里的颜色是我写了一个工具类 是随机选择颜色的
//            values.add(sliceValue);
//        }

        for (int index = 0 ; index < ro._valueLists.size(); index ++) {
            SliceValue sliceValue = new SliceValue((float) ro._valueLists.get(index), ChartUtils.pickColor());
            values.add(sliceValue);
        }
    }


    /**
     * 初始化
     */
    public void initPieChart(ReportObject ro) {
        pieChardata = new PieChartData();
        pieChardata.setHasLabels(true);//显示表情
        pieChardata.setHasLabelsOnlyForSelected(false);//不用点击显示占的百分比
        pieChardata.setHasLabelsOutside(false);//占的百分比是否显示在饼图外面
        pieChardata.setHasCenterCircle(true);//是否是环形显示
        pieChardata.setValues(values);//填充数据
        pieChardata.setCenterCircleColor(Color.WHITE);//设置环形中间的颜色
        pieChardata.setCenterCircleScale(0.5f);//设置环形的大小级别
        pieChardata.setCenterText1(ro.innerLabelName);//环形中间的文字1
        pieChardata.setCenterText1Color(Color.BLACK);//文字颜色
        pieChardata.setCenterText1FontSize(14);//文字大小

//        pieChardata.setCenterText2("饼图测试");
        pieChardata.setCenterText2Color(Color.BLACK);
        pieChardata.setCenterText2FontSize(18);
        /**这里也可以自定义你的字体   Roboto-Italic.ttf这个就是你的字体库*/
//      Typeface tf = Typeface.createFromAsset(this.getAssets(), "Roboto-Italic.ttf");
//      data.setCenterText1Typeface(tf);

        pieChart.setPieChartData(pieChardata);
        pieChart.setValueSelectionEnabled(true);//选择饼图某一块变大
        pieChart.setAlpha(0.9f);//设置透明度
        pieChart.setCircleFillRatio(1f);//设置饼图大小

    }
    public static String getRandColorCode(){
        String r,g,b;
        Random random = new Random();
        r = Integer.toHexString(random.nextInt(256)).toUpperCase();
        g = Integer.toHexString(random.nextInt(256)).toUpperCase();
        b = Integer.toHexString(random.nextInt(256)).toUpperCase();

        r = r.length()==1 ? "0" + r : r ;
        g = g.length()==1 ? "0" + g : g ;
        b = b.length()==1 ? "0" + b : b ;

        return "#" + r+g+b;
    }
}
