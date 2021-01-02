using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class TextureImportChanges : EditorWindow
{

	// コンボボックスのリスト
	public static readonly string[] list = { "32", "64", "128", "256", "512", "1024","2048","4096","8192" };
	
	// コンボボックスのインデックス番号
	public int index = 5;
	
	// Max Size
	[Tooltip("32/64/128/256/512/1024/2048/4096/8192")]
	public int MaxSize = 512;


	[MenuItem("Custom/TextureImportChanges")]
	static void Open()
	{
		GetWindow<TextureImportChanges>();
	}

	void OnGUI()
	{

		index = EditorGUILayout.Popup("最大画像サイズ", index, list);


		EditorGUILayout.LabelField("現在選択中フォルダ", GetCurrentPath());

		if (GUILayout.Button("MaxSizeを一括変換する"))
		{
			Method2();
		}

		if (GUILayout.Button("一覧バックアップ"))
		{
			Method1();
		}


	}




	// ファイル一覧を取得する(PNG/TIF/JPG/PSD/tga)
	string[] getFileList(string path)
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

		list1.AddRange(list2);
		list1.AddRange(list3);
		list1.AddRange(list4);
		list1.AddRange(list5);
		list1.AddRange(list6);
		list1.AddRange(list7);

		string[] files = (string[])list1.ToArray(typeof(string));

		// ファイル名を整形する
		for (int i = 0; i < files.Length; i++)
		{
			files[i] = files[i].Replace("\\", "/");
			files[i] = files[i].Replace(Application.dataPath, "Assets");
		}

		return files;
	}

	//[ContextMenu("MaxSizeの一覧をバックアップする。")]
	void Method1()
	{
		string[] files = getFileList(Application.dataPath + "/");
		if (files.Length == 0)
		{
			Debug.LogWarning("画像ファイルが見つかりませんでした。");
			return;
		}

		for (int i = 0; i < files.Length; i++)
		{
			files[i] = files[i].Replace("\\", "/");
			files[i] = files[i].Replace(Application.dataPath, "Assets");

			TextureImporter Ti = AssetImporter.GetAtPath(files[i]) as TextureImporter;
			files[i] = Ti.maxTextureSize + "," + "\"" + files[i] + "\"";
		}

		// Assets/backup_all.txtを作成する
		File.WriteAllText(Application.dataPath + "/backup_all.txt", string.Join("\n", files));
		Debug.Log("Asset/backup_all.txtを作成しました。");

		// エディタ側のファイル構成を更新する
		AssetDatabase.Refresh();
	}

	//[ContextMenu("MaxSizeを一括変更する。")]
	void Method2()
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

		// string[] files = getFileList(Application.dataPath + "/" + this.AssetsName);

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

			SetImportSettings(Ti);	// インポート設定変更
			
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
	private string GetCurrentPath()
	{
		string rst = "";
		
		// 選択したフォルダのパスを取得（複数選択した場合は初めの１つめだけ）
		Object[] assets = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);

		if (assets.Length == 1)	// １つだけ選択?(Yes) 現在複数選択は無効にしている。
		{
			if (assets[0] is DefaultAsset)
			{
				string path = AssetDatabase.GetAssetPath(assets[0]);

				if (AssetDatabase.IsValidFolder(path))
				{
					//Debug.Log("Valid folder. " + path);
					rst = path;
				}
			}
		}
		
		// 選択したフォルダ内のすべてのアセットを取ってくる。
		// foreach (Object obj in Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.DeepAssets))
		// {
		// 	if (obj is DefaultAsset)
		// 	{
		// 		string path = AssetDatabase.GetAssetPath(obj);
		//
		// 		if (AssetDatabase.IsValidFolder(path))
		// 		{
		// 			//Debug.Log("Valid folder. " + path);
		// 			rst = path;
		// 		}
		// 	}
		// }

		return rst;
	}

	/// <summary>
	/// 画像インポート設定
	/// </summary>
	private void SetImportSettings(TextureImporter importer)
	{
		// Spriteに設定
		importer.textureType = TextureImporterType.Sprite;
		
		// MipMapのチェックを外す
		importer.mipmapEnabled = false;

		// TextureImporterPlatformSettings iPhone_png = new TextureImporterPlatformSettings ();
		// iPhone_png.overridden = true;
		// iPhone_png.name = "iPhone";
		// iPhone_png.maxTextureSize = 2048;
		// iPhone_png.format = TextureImporterFormat.ASTC_RGBA_4x4;
		// iPhone_png.compressionQuality = 50;
		// iPhone_png.allowsAlphaSplitting = false;
		//
		// TextureImporterPlatformSettings iPhone_jpeg = new TextureImporterPlatformSettings ();
		// iPhone_jpeg.overridden = true;
		// iPhone_jpeg.name = "iPhone";
		// iPhone_jpeg.maxTextureSize = 2048;
		// iPhone_jpeg.format = TextureImporterFormat.ASTC_RGB_4x4;
		// iPhone_jpeg.compressionQuality = 50;
		// iPhone_jpeg.allowsAlphaSplitting = false;
		//
		// TextureImporterPlatformSettings Android_png = new TextureImporterPlatformSettings ();
		// Android_png.overridden = true;
		// Android_png.name = "Android";
		// Android_png.maxTextureSize = 2048;
		// Android_png.format = TextureImporterFormat.DXT5;
		// Android_png.compressionQuality = 50;
		// Android_png.allowsAlphaSplitting = false;
		//
		// TextureImporterPlatformSettings Android_jpeg = new TextureImporterPlatformSettings ();
		// Android_jpeg.overridden = true;
		// Android_jpeg.name = "Android";
		// Android_jpeg.maxTextureSize = 2048;
		// Android_jpeg.format = TextureImporterFormat.DXT1;
		// Android_jpeg.compressionQuality = 50;
		// Android_jpeg.allowsAlphaSplitting = false;
		
		TextureImporterPlatformSettings WebGL_png = new TextureImporterPlatformSettings ();
		WebGL_png.overridden = true;
		WebGL_png.name = "WebGL";
		WebGL_png.maxTextureSize = MaxSize;
		WebGL_png.format = TextureImporterFormat.DXT5;
		// WebGL_png.compressionQuality = 50;
		WebGL_png.allowsAlphaSplitting = false;
		
		TextureImporterPlatformSettings WebGL_jpeg = new TextureImporterPlatformSettings ();
		WebGL_jpeg.overridden = true;
		WebGL_jpeg.name = "WebGL";
		WebGL_jpeg.maxTextureSize = MaxSize;
		WebGL_jpeg.format = TextureImporterFormat.DXT1;
		// WebGL_jpeg.compressionQuality = 50;
		WebGL_jpeg.allowsAlphaSplitting = false;


		if (importer.DoesSourceTextureHaveAlpha ()) {
			//Alphaチャンネルがある場合
			// importer.SetPlatformTextureSettings (iPhone_png);
			// importer.SetPlatformTextureSettings (Android_png);
			importer.SetPlatformTextureSettings (WebGL_png);

		} else {
			//Alphaチャンネルがない場合
			// importer.SetPlatformTextureSettings (iPhone_jpeg);
			// importer.SetPlatformTextureSettings (Android_jpeg);
			importer.SetPlatformTextureSettings(WebGL_jpeg);
		}
	}
}