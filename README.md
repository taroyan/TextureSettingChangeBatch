# TextureSettingChangeBatch



## はじめに

- 大量のテクスチャの設定を変えるのがめんどくさかったので、一気に設定を変更するエディタを作成しました。
- 複数テクスチャのImport Settingsの一部の項目を一気に変更します。



## 使い方

- 「Custom」タブを開き、「TextureImportChanges」をクリックして、ウインドウを開く

![](https://taroyan3rd.com/images/TextureSettingChangeBatch/ImportSettingsBatchConversion.JPG)



- 各種設定を行う。
  - Max Size
    - 画像の最大サイズの設定
  - MipMap Enabled
    - MipMapを生成するかどうかのチェック　Import Settingsの「Generate Mip Maps」に相当
  - Default
    - Import SettingsのDefaultタブの設定を変更
  - Override for Standalone
    - Import SettingsのStandaloneタブの設定を変更

  - Override for iOS
    - Import SettingsのiOSタブの設定を変更
  - Override for Android
    - Import SettingsのAndroidタブの設定を変更
  - Override for WebGL
    - Import SettingsのWebGLタブの設定を変更
- 設定変更したいテクスチャが入ったフォルダを選択する
  - 選択すると選択されたフォルダがウィンドウ内に表示されます。
  - 複数フォルダ選択可能です。
  - ファイルを選択しても変換できません。（＝フォルダ選択のみ有効）
- 「Import Settings Batch Conversion」ボタンを押す。
  - フォルダ内のテクスチャ全ての設定が変更されます。



## 細かい設定項目について

- 圧縮フォーマットや圧縮クオリティなどは各プラットフォームごとに適当に決めています。（一応Alpha有り無しで切り分けています）
- もし変更したい場合は、直接スクリプトを編集してください。
- 細かい設定もウィンドウ内で出来るようにしたほうがよかったかもしれません。



## Introduction.

- Changing the settings of a large number of textures is very time consuming, so I created an editor extension that allows you to change the settings all at once.
- You can change some items in the Import Settings of each texture at once.



## How To Use

- Open the "Custom" tab and click "TextureImportChanges" to open the window.

  ![](https://taroyan3rd.com/images/TextureSettingChangeBatch/ImportSettingsBatchConversion.JPG)

- Making various settings

  - - Max Size
      - Setting the maximum size of an image
    - MipMap Enabled
      - Check if MipMap should be generated or not. Equivalent to "Generate Mip Maps" in Import Settings.
    - Default
      - Change the settings in the Default tab of Import Settings.
    - Override for Standalone
      - Change the settings in the Standalonetab of Import Settings.

    - Override for iOS
      - Change the settings in the  iOS of Import Settings.
    - Override for Android
      - Change the settings in the Android of Import Settings.
    - Override for WebGL
      - Change the settings in the WebGL of Import Settings.
  - Select the folder containing the texture you want to change the settings for
    - When selected, the selected folder will appear in the editor window.
    - Multiple folders can be selected.
    - Selecting a file is not available. (= Only folder selection is valid.)
  - Press the "Import Settings Batch Conversion" button
    - This will change the settings for all textures in the folder.

  

  ## For detailed setting items

  - The compression format and quality are fixed for each platform. (In a way, I've separated them into Alpha and non-Alpha.)
  - If you want to change it, you can edit the script directly.
  - It would have been better to be able to make detailed settings in the window.