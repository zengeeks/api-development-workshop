# API を使ってみる

- [API の要点をおさらい](#api-の要点をおさらい)
- [REST クライアントの使い方](#rest-クライアントの使い方)
- [GET リクエストを行う](#get-リクエストを行う)
- [POST リクエストを行う](#post-リクエストを行う)

## API の要点をおさらい

API (Application Programming Interface) で抑えるべきポイントをおさらいしましょう。

なお、ここでは HTTP プロトコルにおける API を取り扱います。

| ポイント | 説明 |
|----|----|
| HTTP リクエスト | |
| HTTP レスポンス | |
| URL | ウェブ上のリソースを識別してアクセスするための、プロトコルやドメイン、ポート、そしてパスを含む文字列。 |
| HTTP メソッド | |
| クエリ |  |
| フラグメント | |
| リクエストボディ | |
| ヘッダー | |
| MIME タイプ | |

HTTP の基本については、下記が参考になります。

- [HTTP の基本 - HTTP | MDN](https://developer.mozilla.org/ja/docs/Web/HTTP/Basics_of_HTTP)
- [Uniform Resource Identifiers (URI) の構文 - ウェブ上のリソースの識別 - HTTP | MDN](https://developer.mozilla.org/ja/docs/Web/HTTP/Basics_of_HTTP/Identifying_resources_on_the_Web#syntax_of_uniform_resource_identifiers_uris)

## REST クライアントの使い方

ここでは、REST クライアントとして Postman を利用します。

![Postman](./images/postman_getting-started.png)

Postman は、複数のワークスペースを切替えて利用することができ、またコレクションという形でHTTPリクエストをまとめることができます。

それではまず、このワークショップの作業をするためのワークスペースを作成しましょう。

Workspaces のプルダウンを開き、「Create Workspace」ボタンを選択します。

![Postman: 新しいワークスペースを作成する](./images/postman_create-workspace_001.png)

「Create workspace」画面で、下記を参考に各項目を設定し、「Create Workspace」ボタンを選択します。

| 項目 | 説明 |
|----|----|
| Name | 適宜ワークスペース名を入力する（例: `API 開発ハンズオン`） |
| Summary | 適宜サマリを記入する |
| Visibility | 公開範囲を指定する（例: `Personsal`） |

![Postman: 新しいワークスペースの設定項目を入力し、作成する](./images/postman_create-workspace_002.png)

つぎに、コレクションを作成します。

「Collections」タブの左メニュー上部にある「+」を選択し、新しいコレクションを作成します。

![Postman: 新しいコレクションを作成する](./images/postman_create-new-collection_001.png)

これで、HTTP リクエストを行う準備ができました。

## GET リクエストを行う

まず、一番シンプルな GET リクエストを送信してみましょう。

REST クライアントに、URL を指定して送信します。

- [ ] TODO: シンプルなURLをたたきたい

つぎに、このURLで GET リクエストを行ってみましょう。

https://petstore3.swagger.io/api/v3/pet/findByStatus

```
No status provided. Try again?
```

実はこの API はクエリが必要です。

https://petstore3.swagger.io/api/v3/pet/findByStatus?status=available

## POST リクエストを行う

