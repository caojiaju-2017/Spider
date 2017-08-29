package tender.tc.hs.tenderclient.HsListView;

import java.io.File;

import android.content.Context;
import android.graphics.Bitmap;
import android.widget.ImageView;

import com.nostra13.universalimageloader.cache.disc.impl.UnlimitedDiscCache;
import com.nostra13.universalimageloader.cache.disc.naming.Md5FileNameGenerator;
import com.nostra13.universalimageloader.cache.memory.impl.UsingFreqLimitedMemoryCache;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.ImageLoaderConfiguration;
import com.nostra13.universalimageloader.core.assist.ImageLoadingListener;
import com.nostra13.universalimageloader.core.assist.ImageSize;
import com.nostra13.universalimageloader.core.assist.QueueProcessingType;
import com.nostra13.universalimageloader.core.download.BaseImageDownloader;
import com.nostra13.universalimageloader.utils.StorageUtils;

import android.os.Environment;

import tender.tc.hs.tenderclient.HsApplication;
import tender.tc.hs.tenderclient.Util.FileUtils;

public class ImageLoadUtil {
//	private static final String TAG = "ImageLoadUtil";

	public static final String IMAGE_DIRECTORY = Environment.getDataDirectory() + File.separator +"MRMF" + File.separator + "image";
	private static ImageLoadUtil iLoadUtil;

	private DisplayImageOptions optionDefault; // DisplayImageOptions


	public static final int DEFAULT_EMPTY = 0;

	private ImageLoadUtil() {
	}

	public static ImageLoadUtil getInstance() {
		if (iLoadUtil == null) {
			iLoadUtil = new ImageLoadUtil();

            // 检查目录是否存在 ,true 表示不存在就创建
            if (FileUtils.isDirectoryExist(IMAGE_DIRECTORY,true))
            {
                //
            }

			File cacheDir =StorageUtils.getOwnCacheDirectory(HsApplication.Global_App.getApplicationContext(), IMAGE_DIRECTORY);
			ImageLoaderConfiguration config = new ImageLoaderConfiguration   
			          .Builder(HsApplication.Global_App.getApplicationContext())
			          .memoryCacheExtraOptions(480, 800) // maxwidth, max height，即保存的每个缓存文件的最大长宽   
			          .threadPoolSize(3)//线程池内加载的数量   
			          .threadPriority(Thread.NORM_PRIORITY -2)   
			          .denyCacheImageMultipleSizesInMemory()   
			           .memoryCache(new UsingFreqLimitedMemoryCache(16* 1024 * 1024)) // You can pass your own memory cache implementation/你可以通过自己的内存缓存实现   
			           .memoryCacheSize(16 * 1024 * 1024)     
			          .discCacheSize(50 * 1024 * 1024)     
			          .discCacheFileNameGenerator(new Md5FileNameGenerator())//将保存的时候的URI名称用MD5 加密   
			           .tasksProcessingOrder(QueueProcessingType.LIFO)   
			           .discCacheFileCount(600) //缓存的文件数量   
			           .discCache(new UnlimitedDiscCache(cacheDir))//自定义缓存路径   
			           .defaultDisplayImageOptions(DisplayImageOptions.createSimple())   
			           .imageDownloader(new BaseImageDownloader(HsApplication.Global_App.getApplicationContext(),5 * 1000, 30 * 1000)) // connectTimeout (5 s), readTimeout (30 s)超时时间   
			           .writeDebugLogs() // Remove for releaseapp   
			          .build();//开始构建   
			ImageLoader.getInstance().init(config);  
//			ImageLoader.getInstance().
		}
		return iLoadUtil;
	}


	public void init(Context ctx) {
		if (!ImageLoader.getInstance().isInited()) {
			ImageLoaderConfiguration config = new ImageLoaderConfiguration.Builder(ctx.getApplicationContext()).build();
			ImageLoader.getInstance().init(config);
		}

	}

	public ImageLoader getImageLoader() {
		return ImageLoader.getInstance();
	}

	/**
	 * @Title: load
	 * @Description: TODO
	 * @param @param path
	 * @param @param imageView
	 * @param @param imageOnLoadingRes
	 * @param @param l  ImageLoaderConfigurationconfig
	 * @return void 
	 * @throws
	 */
	public void load(String path, ImageView imageView, int imageOnLoadingRes, ImageLoadingListener l) {
		if (DEFAULT_EMPTY == imageOnLoadingRes) {
			optionDefault = new DisplayImageOptions.Builder().cacheInMemory(true).cacheOnDisc(true).build();
		} else {
			optionDefault = new DisplayImageOptions.Builder().showImageOnLoading(imageOnLoadingRes).showImageForEmptyUri(imageOnLoadingRes).showImageOnFail(imageOnLoadingRes).cacheInMemory(true).cacheOnDisc(true).build();
		}
		ImageLoader.getInstance().displayImage(path, imageView, optionDefault, l);
	}

	/**
	 * @Title: load
	 * @Description: TODO
	 * @param @param path
	 * @param @param imageView
	 * @param @param l
	 * @return void
	 * @throws
	 */
	public void load(String path, ImageView imageView, ImageLoadingListener l) {
		load(path, imageView, DEFAULT_EMPTY, l);
	}

	public void load(String path, ImageView imageView, int imageOnLoadingRes) {
		load(path, imageView, imageOnLoadingRes, null);
	}

	public void load(String path, ImageView imageView) 
	{
		
		load(path, imageView, DEFAULT_EMPTY, null);
	}

	/**
	 * @Title: loadBitmap
	 * @Description: 
	 * @param @param uri
	 * @param @param targetImageSize
	 * @param @param imageOnLoadingRes
	 * @param @return 
	 * @return Bitmap 
	 * @throws
	 */
	public Bitmap loadBitmap(String uri, ImageSize targetImageSize, int imageOnLoadingRes) {
		DisplayImageOptions options = new DisplayImageOptions.Builder().showImageOnLoading(imageOnLoadingRes).showImageForEmptyUri(imageOnLoadingRes).showImageOnFail(imageOnLoadingRes).cacheInMemory(true).cacheOnDisc(true).build();
		return ImageLoader.getInstance().loadImageSync(uri, targetImageSize, options);
	}

}
