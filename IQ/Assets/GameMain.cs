using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameMain : MonoBehaviour {

	// 正解数
	private static int[] rightAns = { 2, 2, 1 };

	// 画像配置数
	private static int[] imgMax = { 4, 5, 6 };

	// 答えID
	private static int answer_id;
	private struct ANSWERINFO
	{
		public Texture2D t2d;
		public int btnAnsFlag;
	};
	private static ANSWERINFO[] aInfos = null;

	private static Button btnPopup = null;
	private static Image imgPopup = null;

	// クリアフラグ
	private static bool isClear;

	// 以下GameMain.seane上のオブジェクトを制御するインスタンス
	[SerializeField]
	private Image imgQuestion = null;
	[SerializeField]
	private Image imgBtnAns1 = null;
	[SerializeField]
	private Image imgBtnAns2 = null;
	[SerializeField]
	private Image imgBtnAns3 = null;
	[SerializeField]
	private Image imgBtnAns4 = null;
	[SerializeField]
	private Image imgBtnAns5 = null;
	[SerializeField]
	private Image imgBtnAns6 = null;


	// 質問をランダムで取得
	private Texture2D SelectQuestion(){ 
		int question_id = UnityEngine.Random.Range(1,40);
		SqliteDatabase sqlDB = new SqliteDatabase("config.db");
		DataTable dt = sqlDB.ExecuteQuery("select * from Pattern " +
			"WHERE patternid = '"+question_id.ToString()+"'");
		string image_path = null;

		foreach (DataRow dr in dt.Rows) {
			answer_id = (int)dr["answerid"];
			image_path = (string)dr["image_path"];
			break;
		}
		return Resources.Load (image_path) as Texture2D;
	}

	private ANSWERINFO[] SelectAnswer(){
		int level = Common.getLevel()-1;
		if (level < 0) level = 0;

		DataTable dt = Common.ExcuteDB( "SELECT * FROM " +
			"(SELECT Ptn.patternid, Ptn.answerid, Ptn.image_path, 1 AS ansflag FROM " +
			"(select * from Answer WHERE answerid = '" + answer_id.ToString() + "' " +
			"ORDER BY RANDOM() LIMIT " + rightAns [level] + ") AS Ans " +
			"LEFT JOIN Pattern AS Ptn " +
			"ON Ptn.patternid = Ans.patternid " +
			"UNION ALL " +
			"SELECT * FROM " +
			"(SELECT patternid, answerid, image_path, 0 AS ansflag " +
			"FROM Pattern " +
			"WHERE patternid NOT IN " +
			"(SELECT answerid FROM Answer WHERE patternid = '" + answer_id.ToString() + "') " +
			"AND patternid != '" + answer_id.ToString () + "' " +
			"ORDER BY RANDOM() LIMIT "+ (imgMax[level]-rightAns[level]).ToString() + ")) "+
			"ORDER BY RANDOM()"
		);

		int ansCnt = 0;
		aInfos = new ANSWERINFO[imgMax[level]];
		string image_path = null;
		// 正解データ取得のSQL
		foreach (DataRow dr in dt.Rows) {
			aInfos[ansCnt].btnAnsFlag = (int)dr["ansflag"];
			image_path = (string)dr["image_path"];
			aInfos[ansCnt].t2d = Resources.Load (image_path) as Texture2D;
			ansCnt++;
		}
		return aInfos;
	}

	public static void CheckAnswer (int idx){
		int level = Common.getLevel()-1;
		if (level < 0) level = 0;

		// 押されたボタンが配列外なら終了
		if ( imgMax [level] <= idx ) {
			return;
		}
		string pngName;
		Rect rect;
		if (aInfos [idx].btnAnsFlag != 0) {
			Common.addGoodCnt ();
			if (5 <= Common.getGoodCnt ()) {
				pngName = "0052";
				isClear = true;
				SoundManager.Instance.PlaySE ( 3 );
			} else {
				pngName = "0051";
				SoundManager.Instance.PlaySE ( 1 );
			}
			rect = new Rect (0.0f, 0.0f, 700.0f, 231.0f);
		} else {
			pngName = "0050";
			Common.addBadCnt ();
			SoundManager.Instance.PlaySE ( 2 );
			rect = new Rect (0.0f, 0.0f, 600.0f, 231.0f);
		}
		btnPopup.gameObject.SetActive (true);
		imgPopup.sprite = Sprite.Create(
			Resources.Load( pngName ) as Texture2D,
			rect,
			new Vector2(0.3f,0.3f),
					0.0f);
	}

	// ポップアップをクリック
	public static void PopupClick (){
		if (!isClear) {
			Application.LoadLevel ("GameMain");
		} else {
			Application.LoadLevel ("TopMenu");
		}
	}

	void Start () {
		isClear = false;
		foreach (Transform child in GetComponent<Canvas>().transform){
			if(child.name == "btnPopup"){
				btnPopup = child.gameObject.GetComponent<Button>();
				btnPopup.gameObject.SetActive (false);
				foreach (Transform btnChild in btnPopup.transform){
					if(btnChild.name == "imgPopup"){
						imgPopup = btnChild.gameObject.GetComponent<Image> ();
					}
				}
			}
		}

		// ステージの難易度によって処理分岐
		Debug.Log("ステージNo:"+Common.getLevel().ToString());

		Rect defaultPartsRect = new Rect(0.0f, 0.0f, 750.0f, 750.0f);
		imgQuestion.sprite = Sprite.Create(
			SelectQuestion(),
			defaultPartsRect,
			new Vector2(0.3f,0.3f),
			100.0f);

		// レイアウト設定
		Image[] imgBtnAns = {
			imgBtnAns1,
			imgBtnAns2,
			imgBtnAns3,
			imgBtnAns4,
			imgBtnAns5,
			imgBtnAns6,
		};

		int cnt = 0;
		foreach ( ANSWERINFO aInfo in SelectAnswer () ){
			Debug.Log(aInfo.btnAnsFlag);
				imgBtnAns[cnt].sprite = Sprite.Create(
				aInfo.t2d,
				defaultPartsRect,
				new Vector2(0.3f,0.3f),
				100.0f);
			cnt++;
		}
	}
}
