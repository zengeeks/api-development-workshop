# API を使ってみる

- [API の要点をおさらい](#api-の要点をおさらい)
- [REST クライアントの使い方](#rest-クライアントの使い方)
- [GET リクエストを行う](#get-リクエストを行う)
- [POST リクエストを行う](#post-リクエストを行う)

## API の要点をおさらい

API (Application Programming Interface) で抑えるべきポイントをおさらいしましょう。

なお、ここでは HTTP プロトコルにおける API を取り扱います。

### HTTP プロトコルにおける API

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
| Content-Type | |

HTTP の基本については、下記が参考になります。

- [HTTP の基本 - HTTP | MDN](https://developer.mozilla.org/ja/docs/Web/HTTP/Basics_of_HTTP)
- [Uniform Resource Identifiers (URI) の構文 - ウェブ上のリソースの識別 - HTTP | MDN](https://developer.mozilla.org/ja/docs/Web/HTTP/Basics_of_HTTP/Identifying_resources_on_the_Web#syntax_of_uniform_resource_identifiers_uris)

### RESTful API

TODO: 軽く紹介する

### CRUD

TODO: 軽く紹介する

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

必要に応じて、右クリックのメニューから「Rename」を選択し、コレクション名を更新することができます。

![Postman: コレクション名を変更する](./images/postman_rename-collection_001.png)

![Postman: コレクション名を変更する](./images/postman_rename-collection_002.png)

このコレクションの中に、HTTP リクエストを作成していきます。

リクエストを作成するには、コレクション名を右クリックしたメニューから選択したり、メインペインのタブの「+」から行います。

![Postman: HTTPリクエストを作成する](./images/postman_add-request_001.png)

これで、HTTP リクエストを行う準備ができました。

## GET リクエストを行う

まず手始めに、一番シンプルな GET メソッドのリクエストを送信してみましょう。

GET メソッドは、一般にデータを取得するときに利用します。RESTful API 

それでは、REST クライアントに、URL とメソッドを指定して送信します。

| 項目 | 説明 |
|----|----|
| メソッド | `GET` |
| URL | `https://example.com/items` |

つぎに、クエリを付与したリクエストを送信してみましょう。

| 項目 | 説明 |
|----|----|
| メソッド | `GET` |
| URL | `https://example.com/items?category=kitchen` |

このように、検索条件など秘匿する必要がない入力値はクエリとして指定することが多いです。

また、URLのパスに情報を含めることもあります。これは後の実践でご紹介します。

## POST リクエストを行う

つぎに、POST メソッドのリクエストを送信してみましょう。

POST メソッドによるリクエストは、一般的にクライアントからサーバーにデータを渡す場合に利用されます。

試しに、POST メソッドのリクエストを、REST クライアントから送信してみましょう。

| 項目 | 説明 |
|----|----|
| メソッド | `POST` |
| URL | `https://example.com/items` |
| ヘッダー | `Content-Type`: `application/x-www-form-urlencoded` |
| 送信するデータ |  |

これは、HTML から POST メソッドのリクエストを送る際に利用される `<form>` タグを利用したリクエストを想定した例です。`<form>` タグを利用する場合、リクエストのMIMEタイプには `application/x-www-form-urlencoded` が指定されます。（なお、`<form>` の MIMEタイプは、`enctype` 属性で他の MINEタイプに変更することが可能です。）

また、近年主流になりつつあるフロントエンドとバックエンド（サーバーサイド）を分けた構成のWebアプリケーションでは、サーバーにデータを送信する際、JSONデータを POST メソッドで送ることが多いです。この場合は、 `Content-Type` に `application/json` を指定することで、サーバーに送るデータの型を伝えています。

| 項目 | 説明 |
|----|----|
| メソッド | `POST` |
| URL | `https://example.com/items` |
| ヘッダー | `Content-Type`: `application/json` |
| Request body | `{}` |

このほか、`PUT`, `DELETE` などのメソッドがありますが、ここでは割愛し、次の学習に進みます。

[次へ](./learn-openapi.md)

----

[目次へ戻る](./selfpaced-handson.md)