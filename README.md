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