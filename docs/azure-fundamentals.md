# Azure 基礎

ここでは、Azure を初めて利用する方向けの情報を掲載します。

#### 目次

- [ドキュメントリンク集: API の構成に利用できる Azure サービス](#ドキュメントリンク集-api-の構成に利用できる-azure-サービス)
- [学習コンテンツ: Microsoft Learn](#学習コンテンツ-microsoft-learn)
- [参考: マイクロソフトの公式サイトまとめ情報](#参考-マイクロソフトの公式サイトまとめ情報)

## ドキュメントリンク集: API の構成に利用できる Azure サービス

### Azure Functions

Azure Functions はイベント駆動でコードを実行する機構のサービスで、 FaaS (Funtion as a Service) に分類されます。

HTTP ベースの API を作成するには、「HTTP トリガー」を利用します。

- [Azure Functions サーバーレス コンピューティング | Microsoft Azure](https://azure.microsoft.com/ja-jp/services/functions/)
- [Azure Functions の概要 | Microsoft Docs](https://docs.microsoft.com/ja-jp/azure/azure-functions/functions-overview)

### Azure App Service

Azure App Service は Web アプリケーションや REST API をホストできる HTTP ベースのサービスで、 PaaS (Platform as a Service) に分類されます。

- [App Service - Web アプリの構築とホスト | Microsoft Azure](https://azure.microsoft.com/ja-jp/services/app-service/)
- [概要 - Azure App Service | Microsoft Docs](https://docs.microsoft.com/ja-jp/azure/app-service/overview)

### Azure Logic Apps

Azure Logic Apps は、ローコードでさまざまなサービスを連結したり、処理を行うことができるサービスです。

Logic Apps に組み込まれている「Request トリガー」と「Response アクション」を用いて、API を作成することができます。

- [Logic App Service – IPaaS | Microsoft Azure](https://azure.microsoft.com/ja-jp/services/logic-apps/)
- [Azure Logic Apps の概要 - Azure Logic Apps | Microsoft Docs](https://docs.microsoft.com/ja-jp/azure/logic-apps/logic-apps-overview)

### Azure API Management

Azure API Management は、既存のバックエンドのサービスに対して API ゲートウェイを作成することができます。

- [API Management – API の管理 | Microsoft Azure](https://azure.microsoft.com/ja-jp/services/api-management/)
- [Azure API Management の概要と主な概念 | Microsoft Docs](https://docs.microsoft.com/ja-jp/azure/api-management/api-management-key-concepts)

## 学習コンテンツ: Microsoft Learn

Microsoft Learn は、Microsoft が提供する学習コンテンツです。Microsoft 製品だけでなく、IT技術に関する幅広い学習ができる良質なプラットフォームです。

以下におすすめのラーニングパス、モジュールをご紹介します。また、自由に Microsoft Learn のコンテンツを探したい場合は、こちらから覗いてみてください。

- [すべて参照 - Learn | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/browse/)

### おすすめ ラーニングパス

- [Azure の基礎 第 1 部:Azure の主要概念に関する説明 | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/paths/az-900-describe-cloud-concepts/)
- [Azure の基礎 第 2 部:Azure の主要サービスに関する説明 | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/paths/az-900-describe-core-azure-services/)

### 用途別おすすめ ラーニングパス、モジュール

#### ウェブアプリケーションを作りたい

ウェブアプリケーションを稼働するには、PaaS である Azure App Service がおすすめです。

- [Azure App Service を試す | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/modules/introduction-to-azure-app-service/)
- [Web アプリの設定を構成する | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/modules/configure-web-app-settings/)

#### 静的サイトを公開したい

フロントエンドの静的サイトを公開したい場合は、Azure Static Web Apps がおすすめです。

- [Azure Static Web Apps | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/paths/azure-static-web-apps/)

#### 画像などのファイルを格納したい

画像やバイナリファイルをストレージに格納したい場合は、Azure Storage Account の Blob storage がおすすめです。

- [Azure Storage アカウントを作成する | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/modules/create-azure-storage-account/)
- [BLOB ストレージを構成する | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/modules/configure-blob-storage/)
- [ツールを使用したストレージの構成 | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/modules/configure-storage-tools/)

Azure Storage Account には、 Blob storage の他に、ファイル共有、Queue storage などがあります。知りたい方は下記をご参考ください。

- [ストレージ アカウントの構成 | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/modules/configure-storage-accounts/)
- [Azure ストレージ サービスについて | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/modules/azure-storage-fundamentals/)

#### ローコードツールで処理を作成したい

ローコードでさまざまなサービスを連結したり、処理を行いたい場合は Azure Logic Apps がおすすめです。

- [Azure Logic Apps でデータとアプリを統合するための自動化されたワークフローを構築する | Microsoft Docs](https://docs.microsoft.com/ja-jp/learn/paths/build-workflows-with-logic-apps/)

## 参考: マイクロソフトの公式サイトまとめ情報

Microsoft の社員の方がまとめられた学習者向けのプレゼンテーションについて、とても参考になるので共有します。

- [冬休みにAzureを学習したい方へ！おすすめマイクロソフト公式サイトまとめ情報](https://www.slideshare.net/RieMoriguchi/azure-250856110)
