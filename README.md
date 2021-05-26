# VRCAvatars3Validator
[![Release](https://github.com/gatosyocora/VRCAvatars3Validator/actions/workflows/release.yml/badge.svg)](https://github.com/gatosyocora/VRCAvatars3Validator/actions/workflows/release.yml)

[JP](#jp) / [EN](#en)

<p id="jp"></p>
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
- **Enable Rules** : チェックが入っているルールで検査します。これらのチェックの有無は専用ウィンドウと同期します

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

---
<p id="en"></p>
Unity Editor extension to detect errors of VRChat avatar

It can add new detection rule.

## How to use

### VRCAvatars3Validator tool window

It open tool window from `VRCAvatars3Validator/Editor` at Unity top menu.

1. select avatar in `Avatar`
2. put a check rules you want to use of `Rules`
3. press `Validate`, show errors
4. press `Select`, select object had error. press `AutoFix`, fix automatically if can fix.

- When upload avatar, show this window if avatar has error by validate.

### VRCAvatars3Validator setting window

It open tool setting window from `Edit> Project Settings> VRCAvatars3Validator` at Unity top menu.

- **Validate OnUploadAvatar** : validate automatically on upload avatar if put a check
- **Enable Rules** : it use rule that put a check in validation.

## Add new rule

1. It duplicate template file and create rule script. You can select way of duplicate.
    - Select `Create> C# VRCAvatars3ValidatorRule` in Project window
    - Select `Assets> Create> C# VRCAvatars3ValidatorRule` at Unity top menu.
    - Duplicate `TemplateRule.cs` file in Rules folder by `Left click> Duplicate`
2. Put new rule script in Rules folder.
3. Reopen tool window.

- Please PR to github repository if add new rule
<https://github.com/gatosyocora/VRCAvatars3Validator>

## Environment

Unity 2018.4.20f1

## Dependence packages

It needs to install those package if use this tool.

- VRCSDK3-AVATAR-xxxx.xx.xx.xx.xx_Public.unitypackage

## Terms of use

The author gatosyocora cannot be held responsible for any problems that may arise from using this tool. 

This tool is MIT License 
It reads [LICENSE](https://github.com/gatosyocora/VRCAvatars3Validator/blob/master/LICENSE) in detail.

## Contact

- Twitter : [@gatosyocora](https://twitter.com/gatosyocora)
- Discord : gatosyocora#9575
