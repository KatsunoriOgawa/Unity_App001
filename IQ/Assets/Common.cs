using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;

/**
 * 共通インスタンス、メソッドを定義
 */

// 値の保持などに使用する
public class Common : MonoBehaviour {

	// StreamingAssetsのフォルダ

	public static string getSaPath (string filename) {
		return System.IO.Path.Combine (Application.streamingAssetsPath, filename);
	}

	// DBを読み込む
	public static SqliteDatabase sqlDB = new SqliteDatabase("config.db");

	private static bool animFlag = false;

	public static bool getAnimFlag () {
		return animFlag;
	}
	public static void setAnimFlag (bool flag) {
		animFlag = flag;
	}

	// 選択したレベル情報を保持するインスタンス
	private static int level = 0;
	
	public static int getLevel () {
		return level;
	}
	public static void setLevel (int slv) {
		level = slv;
	}

	// 選択したレベル情報を保持するインスタンス
	private static int goodCnt = 0;

	public static int getGoodCnt () {
		return goodCnt;
	}
	public static void setGoodCnt (int cnt) {
		goodCnt = cnt;
	}
	public static void addGoodCnt () {
		goodCnt++;
	}

	// 選択したレベル情報を保持するインスタンス
	private static int badCnt = 0;

	public static int getBadCnt () {
		return badCnt;
	}
	public static void setBadCnt (int cnt) {
		badCnt = cnt;
	}
	public static void addBadCnt () {
		badCnt++;
	}

	public static DataTable ExcuteDB( string sql ){

		Debug.Log(sql);
		// SQL実行
		return sqlDB.ExecuteQuery(sql);
	}

}