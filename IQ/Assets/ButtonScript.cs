using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ButtonScript : MonoBehaviour {

	private SpriteRenderer spriteImg;

	[SerializeField]
	private int id;

	public static void btnLevelClick( int level ){
		Debug.Log ("Clicked.");
		if (Common.getAnimFlag ()) {
			Common.setLevel (level);
			// 決定音を鳴らす
			SoundManager.Instance.PlaySE(0);
			Common.setGoodCnt (0);
			Common.setBadCnt (0);
			Application.LoadLevel ("GameMain");
		} else {
			Common.setAnimFlag (true);
		}
	}

	/** レベルの設定系 */
	public void BtnLevelClick () {
		btnLevelClick ( this.id );
	}

	/** GameMain → TopMenuへ */
	public void BtnBackClick () {
		Debug.Log ("Clicked.");
		Common.setGoodCnt (0);
		Common.setBadCnt (0);
		Application.LoadLevel ("TopMenu");
	}

	/** 回答ボタン押下時 */
	public void BtnAnsClick () {
		GameMain.CheckAnswer( id );
	}

	/** ポップアップボタン押下時 */
	public void BtnPopupClick () {
		GameMain.PopupClick();
	}
}