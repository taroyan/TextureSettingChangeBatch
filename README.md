# DefaultSteamVRInput



## はじめに

- SteamVRプラグインの2.xのコントローラー制御がわかりずらかったので、とりあえずコントローラーの入力だけを検知するスクリプトを作成しました。
- 初期設定でバインドされるアクションをすべて取得します。



## 準備

- 以下の記事を参考に、記事内の「実行」の前までの設定を行う
  - [http://kan-kikuchi.hatenablog.com/entry/Unity_VIVE_Getting_Started](http://kan-kikuchi.hatenablog.com/entry/Unity_VIVE_Getting_Started)



## 使い方

- 当ファイル「DefaultSteamVRInput.cs」をプロジェクト内のAssetsフォルダ内の好きな場所を配置する
- SteamVRフォルダ内にある「Simple Sample」のシーンを開く
- ヒエラルキー内の「[CameraRIg]」のオブジェクトに「DefaultSteamVRInput.cs」スクリプトをアタッチする。
- インスペクター内の「DefaultSteamVRInput (Script)」内の以下の項目をコンボボックスから設定
  - activateActionSetOnAttach_Platformer		【\actions\platformer】
  - activateActionSetOnAttach_Buggy　　　　【\actions\buggy】
  - activateActionSetOnAttach_Mixedreality　【\actions\mixedreality】

![](https://taroyan3rd.com/images/DefaultSteamVRInput/A.PNG)

- Unity Editorで実行し、コントローラーのトリガーを引いたりトラックパッドをクリックしたりすると、コンソール画面に検知したInputのデバッグログが表示されます。

- 「Simple Sample」シーン以外でも「DefaultSteamVRInput.cs」を「[CameraRig]」に相当するゲームオブジェクトにアタッチすれば同様のインプットが取得できます。



## 参考

[https://qiita.com/kyourikey/items/232f7810769c7727c9bd](https://qiita.com/kyourikey/items/232f7810769c7727c9bd)
