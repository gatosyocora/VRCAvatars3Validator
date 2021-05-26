# VRCAvatars3Validator

VRChatのAvatars3.0のアバターで起きがちなミスをチェックできるEditor拡張です

チェックするためのルールを新規に作成して自由に追加できます

## 使い方

### VRCAvatars3Validator専用ウィンドウ

Unity上部メニューの`VRCAvatars3Validator/Editor`から専用のウィンドウを開きます

1. Avatarにセットアップ済みのAvatars3.0のアバターを選択します
2. Rulesの検査したいルールにチェックを入れます
3. Validateボタンを押すとErrorsに結果が表示されます
4. Selectを押すと対象のオブジェクトが選択され, 可能なものはAutoFixで直せます

- アバターアップロード時にルールの検査に引っかかる場合はアップロードが中止されて、本ウィンドウが表示されます

### Project Settings/VRCAvatars3Validator

Unity上部メニューの`Edit> Project Settings> VRCAvatars3Validator`で開くことができる設定ウィンドウです

- **Validate OnUploadAvatar** : チェックをいれることでアバターアップロード時に自動テストを行います
- **Enable Rules** : チェックが入っているルールで検査します。これらのチェックの有無は

## ルールの追加

1. ルールのテンプレートを用いて新規のルールスクリプトを作成する。以下のいずれかの方法でテンプレートから作成できます
    - Projectウィンドウで`Create> C# VRCAvatars3ValidatorRule`を選択する
    - Unity上部メニューから`Assets> Create> C# VRCAvatars3ValidatorRule`を選択する
    - Rulesフォルダにある`TemplateRule.cs`を`右クリック> Duplicate`で複製する
2. VRCAvatars3ValidatorフォルダにあるRulesフォルダに作成したルールスクリプトをいれる
3. 専用ウィンドウを開きなおす

- ルールを追加した際にはぜひgithubのリポジトリにPRをください
<https://github.com/gatosyocora/VRCAvatars3Validator>

## 動作保証環境

Unity 2018.4.20f1

## 依存パッケージ

このツールをUnityにインストールして使用する際には以下のものもインストールしてください

- VRCSDK3-AVATAR-xxxx.xx.xx.xx.xx_Public.unitypackage

## 利用規約

本ツールを使用して発生した問題に対して、  
著作者gatosyocoraは一切の責任を負いかねますので、  
あらかじめご了承ください

本ツールはMITライセンスで運用されます  
詳しくは[LICENSE](https://github.com/gatosyocora/VRCAvatars3Validator/blob/master/LICENSE)をご覧ください

## 連絡先

- Twitter : [@gatosyocora](https://twitter.com/gatosyocora)
- Discord : gatosyocora#9575
