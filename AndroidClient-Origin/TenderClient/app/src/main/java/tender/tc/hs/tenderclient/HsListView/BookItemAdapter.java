package tender.tc.hs.tenderclient.HsListView;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.List;

import tender.tc.hs.tenderclient.Data.BookData;
import tender.tc.hs.tenderclient.R;

/**
 * Created by jiaju on 2017-08-19.
 */

public class BookItemAdapter extends BaseAdapter {
    private List<BookData> list = null;
    Context mCtxContext;

    public BookItemAdapter(Context mContext, List<BookData> list) {
        super();
        this.list = list;
        this.mCtxContext = mContext;
    }

    public void updateListView(List<BookData> list) {
        this.list = list;
        notifyDataSetChanged();
    }
    @Override
    public int getCount() {
        return list.size();
    }

    @Override
    public Object getItem(int position) {
        return this.list.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View view, ViewGroup viewGroup) {
        final BookData getInfo = list.get(position);

        LayoutInflater inflater = (LayoutInflater) mCtxContext
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        // 使用View的对象itemView与R.layout.item关联
        View itemView = inflater.inflate(R.layout.book_item, null);
        itemView.setTag(getInfo);

        TextView location_tender = (TextView) itemView.findViewById(R.id.location_tender);
        TextView type_tender = (TextView) itemView.findViewById(R.id.type_tender);
        TextView projectno_tender = (TextView) itemView.findViewById(R.id.projectno_tender);
        TextView rtime_tender = (TextView) itemView.findViewById(R.id.rtime_tender);
        TextView prjname_tender = (TextView) itemView.findViewById(R.id.prjname_tender);

        location_tender.setText(getInfo._classfic);
        type_tender.setText(getInfo._way);
        projectno_tender.setText(getInfo._projectNo);
        rtime_tender.setText(getInfo._time);

        if (getInfo._projectName.length() <= 18)
        {
            prjname_tender.setText(getInfo._projectName);
        }
        else
        {
            prjname_tender.setText(getInfo._projectName.substring(0,17) + "...");
        }


        // 调整按钮位置
        return itemView;
    }
}
