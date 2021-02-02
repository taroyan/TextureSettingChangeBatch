using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class TextureImportChanges : EditorWindow
{
	// Max Size
	[Tooltip("32/64/128/256/512/1024/2048/4096/8192")]
	public int MaxSize = 512;

	// Mipmapを入れるかどうかのチェックボックス
	public bool chkMipMap;

	// Defaultのインポートセッティングを変更するかどうか
	public bool chkDefault;

	// Override for Standalone
	public bool chkStandalone;

	// Override for iOS
	public bool chkiOS;

	// Override for Android 
	public bool chkAndroid;

	// Override for WebGL
	public bool chkWebGL;

	// コンボボックスのリスト
	public static readonly string[] list =
	{
		"32", "64", "128", "256", "512", "1024", "2048", "4096", "8192"
	};

	// コンボボックスのインデックス番号
	public int index = 5;



	// スクロール位置
	private Vector2 _scrollPosition = Vector2.zero;

	// ボタンサイズ
	Vector2 buttonMinSize = new Vector2(100, 20);
	Vector2 buttonMaxSize = new Vector2(1000, 50);


	[MenuItem("Custom/TextureImportChanges")]
	static void Open()
	{
		GetWindow<TextureImportChanges>();
	}

	void OnGUI()
	{
		// 選択したコンボボックスのインデックスを取得
		index = EditorGUILayout.Popup("Max Size", index, list);

		// MipMapを入れるかどうか
		chkMipMap = EditorGUILayout.Toggle("MipMap Enabled", chkMipMap);

		// Default Settings
		chkDefault = EditorGUILayout.Toggle("Default", chkDefault);

		// Override for Standalone
		chkStandalone = EditorGUILayout.Toggle("Override for Standalone", chkStandalone);

		// Override for iOS
		chkiOS = EditorGUILayout.Toggle("Override for iOS", chkiOS);

		// Override for Android
		chkAndroid = EditorGUILayout.Toggle("Override for Android", chkAndroid);

		// Override for WebGL
		chkWebGL = EditorGUILayout.Toggle("Override for WebGL", chkWebGL);


		//描画範囲が足りなければスクロール出来るように
		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

		/*スクロールする描画処理*/
		foreach (var path in GetCurrentPath())
		{
			EditorGUILayout.LabelField("Selected Folder", path);
		}

		//スクロール箇所終了
		EditorGUILayout.EndScrollView();


		// ImortSettings一括処理
		if (GUILayout.Button("Import Settings Batch Conversion",
			GUILayout.MinWidth(buttonMinSize.x), GUILayout.MinHeight(buttonMinSize.y),
			GUILayout.MaxWidth(buttonMaxSize.x), GUILayout.MaxHeight(buttonMaxSize.y)))
		{
			ChangeImportSettings();
		}
	}

	// ファイル一覧を取得する(PNG/TIF/JPG/PSD/tga)
	string[] getFileList(List<string> pathList)
	{

		System.Collections.ArrayList listAll = new ArrayList(); // 全てのファイルArrayList

		foreach (var path in pathList)
		{

			if (!Directory.Exists(path)) return new string[0];

			// PNG
			string[] png = System.IO.Directory.GetFiles(
				path, "*.png", System.IO.SearchOption.AllDirectories);

			// TIF
			string[] tif = System.IO.Directory.GetFiles(
				path, "*.tif", System.IO.SearchOption.AllDirectories);

			// TIFF
			string[] tiff = System.IO.Directory.GetFiles(
				path, "*.tiff", System.IO.SearchOption.AllDirectories);

			// JPG
			string[] jpg = System.IO.Directory.GetFiles(
				path, "*.jpg", System.IO.SearchOption.AllDirectories);

			// JPEG
			string[] jpeg = System.IO.Directory.GetFiles(
				path, "*.jpeg", System.IO.SearchOption.AllDirectories);

			// PSD
			string[] psd = System.IO.Directory.GetFiles(
				path, "*.psd", System.IO.SearchOption.AllDirectories);

			// tga
			string[] tga = System.IO.Directory.GetFiles(
				path, "*.tga", System.IO.SearchOption.AllDirectories);


			System.Collections.ArrayList list1 = new System.Collections.ArrayList(png);
			System.Collections.ArrayList list2 = new System.Collections.ArrayList(tif);
			System.Collections.ArrayList list3 = new System.Collections.ArrayList(tiff);
			System.Collections.ArrayList list4 = new System.Collections.ArrayList(jpg);
			System.Collections.ArrayList list5 = new System.Collections.ArrayList(jpeg);
			System.Collections.ArrayList list6 = new System.Collections.ArrayList(psd);
			System.Collections.ArrayList list7 = new System.Collections.ArrayList(tga);

			listAll.AddRange(list1);
			listAll.AddRange(list2);
			listAll.AddRange(list3);
			listAll.AddRange(list4);
			listAll.AddRange(list5);
			listAll.AddRange(list6);
			listAll.AddRange(list7);

		}

		string[] files = (string[])listAll.ToArray(typeof(string));

		// ファイル名を整形する
		for (int i = 0; i < files.Length; i++)
		{
			files[i] = files[i].Replace("\\", "/");
			files[i] = files[i].Replace(Application.dataPath, "Assets");
		}

		return files;
	}

	//[ContextMenu("ImportSettingsを一括変更する。")]
	void ChangeImportSettings()
	{
		switch (this.index)
		{
			case 0:
				MaxSize = 32;
				break;
			case 1:
				MaxSize = 64;
				break;
			case 2:
				MaxSize = 128;
				break;
			case 3:
				MaxSize = 256;
				break;
			case 4:
				MaxSize = 512;
				break;
			case 5:
				MaxSize = 1024;
				break;
			case 6:
				MaxSize = 2048;
				break;
			case 7:
				MaxSize = 4096;
				break;
			case 8:
				MaxSize = 8192;
				break;
			default:
				Debug.LogWarning("MaxSizeは32/64/128/256/512/1024/2048/4096/8192のいずれかを設定します。");
				return;
		}

		string[] files = getFileList(GetCurrentPath()); // 現在選択中のフォルダ下のファイルを取得する

		if (files.Length == 0)
		{
			Debug.LogWarning("画像ファイルが見つかりませんでした。アセット名を確認してください。");
			return;
		}

		for (int i = 0; i < files.Length; i++)
		{
			// MaxSizeを設定する
			// ※既にMaxSize以下の画像はそのままとする
			TextureImporter Ti = AssetImporter.GetAtPath(files[i]) as TextureImporter;

			// if (Ti.maxTextureSize > this.MaxSize)
			// {

			SetImportSettings(Ti); // インポート設定変更

			// 変更内容をプロジェクトに反映する ※この処理は数分かかります。                
			// (注意)この3種のメソッドの使い方は正しくないかも知れません。
			// (注意)とりあえず、MaxSizeを下げる分には動いているのでこのままにしておきます。
			EditorUtility.SetDirty(Ti);
			AssetDatabase.SaveAssets();
			AssetDatabase.ImportAsset(files[i], ImportAssetOptions.ForceUpdate);
			// }
		}

		Debug.Log("一括変更を行いました。変換ファイル数は" + files.Length);
	}

	/// <summary>
	/// 現在選択しているフォルダのパスを取得する
	/// </summary>
	private List<string> GetCurrentPath()
	{
		List<string> rstList = new List<string>();

		// 選択したフォルダのパスを取得（複数選択した場合は初めの１つめだけ）
		Object[] assets = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);

		foreach (var asset in assets)
		{
			if (asset is DefaultAsset)
			{
				string path = AssetDatabase.GetAssetPath(asset);

				if (AssetDatabase.IsValidFolder(path))
				{
					rstList.Add(path);
				}
			}
		}

		return rstList;
	}

	/// <summary>
	/// 画像インポート設定
	/// </summary>
	private void SetImportSettings(TextureImporter importer)
	{
		// Spriteに設定
		importer.textureType = TextureImporterType.Sprite;

		// MipMapのチェック
		importer.mipmapEnabled = chkMipMap;

		if (chkDefault)
		{
			importer.maxTextureSize = MaxSize;
		}

		// Standalone
		TextureImporterPlatformSettings Standalone_png = new TextureImporterPlatformSettings();
		Standalone_png.overridden = chkStandalone;
		Standalone_png.name = "Standalone";
		Standalone_png.maxTextureSize = MaxSize;
		Standalone_png.format = TextureImporterFormat.DXT5;
		Standalone_png.compressionQuality = 50;
		Standalone_png.allowsAlphaSplitting = false;

		TextureImporterPlatformSettings Standalone_jpeg = new TextureImporterPlatformSettings();
		Standalone_jpeg.overridden = chkStandalone;
		Standalone_jpeg.name = "Standalone";
		Standalone_jpeg.maxTextureSize = MaxSize;
		Standalone_jpeg.format = TextureImporterFormat.DXT1;
		Standalone_jpeg.compressionQuality = 50;
		Standalone_jpeg.allowsAlphaSplitting = false;

		// iOS
		TextureImporterPlatformSettings iPhone_png = new TextureImporterPlatformSettings();
		iPhone_png.overridden = chkiOS;
		iPhone_png.name = "iPhone";
		iPhone_png.maxTextureSize = MaxSize;
		iPhone_png.format = TextureImporterFormat.ASTC_RGBA_4x4;
		iPhone_png.compressionQuality = 50;
		iPhone_png.allowsAlphaSplitting = false;

		TextureImporterPlatformSettings iPhone_jpeg = new TextureImporterPlatformSettings();
		iPhone_jpeg.overridden = chkiOS;
		iPhone_jpeg.name = "iPhone";
		iPhone_jpeg.maxTextureSize = MaxSize;
		iPhone_jpeg.format = TextureImporterFormat.ASTC_RGB_4x4;
		iPhone_jpeg.compressionQuality = 50;
		iPhone_jpeg.allowsAlphaSplitting = false;

		// Android
		TextureImporterPlatformSettings Android_png = new TextureImporterPlatformSettings();
		Android_png.overridden = chkAndroid;
		Android_png.name = "Android";
		Android_png.maxTextureSize = MaxSize;
		Android_png.format = TextureImporterFormat.DXT5Crunched;
		Android_png.compressionQuality = 50;
		Android_png.allowsAlphaSplitting = false;

		TextureImporterPlatformSettings Android_jpeg = new TextureImporterPlatformSettings();
		Android_jpeg.overridden = chkAndroid;
		Android_jpeg.name = "Android";
		Android_jpeg.maxTextureSize = MaxSize;
		Android_jpeg.format = TextureImporterFormat.DXT1Crunched;
		Android_jpeg.compressionQuality = 50;
		Android_jpeg.allowsAlphaSplitting = false;

		// WebGL
		TextureImporterPlatformSettings WebGL_png = new TextureImporterPlatformSettings();
		WebGL_png.overridden = chkWebGL;
		WebGL_png.name = "WebGL";
		WebGL_png.maxTextureSize = MaxSize;
		WebGL_png.format = TextureImporterFormat.DXT5Crunched;
		// WebGL_png.compressionQuality = 50;
		WebGL_png.allowsAlphaSplitting = false;

		TextureImporterPlatformSettings WebGL_jpeg = new TextureImporterPlatformSettings();
		WebGL_jpeg.overridden = chkWebGL;
		WebGL_jpeg.name = "WebGL";
		WebGL_jpeg.maxTextureSize = MaxSize;
		WebGL_jpeg.format = TextureImporterFormat.DXT1Crunched;
		// WebGL_jpeg.compressionQuality = 50;
		WebGL_jpeg.allowsAlphaSplitting = false;


		// ImportSettingをセットする
		if (importer.DoesSourceTextureHaveAlpha())
		{
			//Alphaチャンネルがある場合
			importer.SetPlatformTextureSettings(Standalone_png);
			importer.SetPlatformTextureSettings(iPhone_png);
			importer.SetPlatformTextureSettings(Android_png);
			importer.SetPlatformTextureSettings(WebGL_png);
		}
		else
		{
			//Alphaチャンネルがない場合
			importer.SetPlatformTextureSettings(Standalone_jpeg);
			importer.SetPlatformTextureSettings(iPhone_jpeg);
			importer.SetPlatformTextureSettings(Android_jpeg);
			importer.SetPlatformTextureSettings(WebGL_jpeg);
		}
	}
}