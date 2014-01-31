// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sample.cs" company="Tadahiro Ishisaka">
//   License:
//   
//     Copyright 2014 - 2014 Tadahiro Ishisaka
//   
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//   
//         http://www.apache.org/licenses/LICENSE-2.0
//    
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
// <summary>
//   OctoKit.NETを使用したGitHubアクセスのサンプル
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace OctokitSample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;

    using Octokit;
    using Octokit.Internal;

    /// <summary>
    ///     OctoKit.NETを使用したGitHubアクセスのサンプル
    /// </summary>
    public class Sample
    {
        #region Public Methods and Operators

        /// <summary>
        /// 認証ログインのサンプル
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        public async void AuthenticationSample(string password)
        {
            // Basic認証のオブジェクトを作成する
            // OAuthのTokenを引数に与えても良い
            var credential = new Credentials("ishisaka", password);

            // 認証付きでGutHUbのクライアントを作成
            var github = new GitHubClient(
                new ProductHeaderValue("IhsisakaSample"), 
                new InMemoryCredentialStore(credential));

            // イシューを取得する。オーナー名、レポジトリ名を引数に
            IReadOnlyList<Issue> issues = await github.Issue.GetForRepository("ishisaka", "juzshizuoka");
            foreach (Issue issue in issues)
            {
                Console.WriteLine("Number:\t{0}", issue.Number);
                Console.WriteLine("Title:\t{0}", issue.Title);
                Console.WriteLine("Date:\t{0}", issue.CreatedAt);
                Console.WriteLine("Body: \r\n{0}", issue.Body);
                Console.WriteLine("User:\t{0}", issue.User.Login);
                Console.WriteLine("--------");
            }
        }

        /// <summary>
        ///     レポジトリのIssueを取得するサンプル
        /// </summary>
        public async void GetIssues()
        {
            // クライアントを作成する。公開レポジトリなら認証は必要なし
            var github = new GitHubClient(new ProductHeaderValue("IshisakaSample"));

            // イシューを取得する。オーナー名、レポジトリ名を引数に
            IReadOnlyList<Issue> issues = await github.Issue.GetForRepository("ishisaka", "nodeintellisense");
            foreach (Issue issue in issues)
            {
                Console.WriteLine("Number:\t{0}", issue.Number);
                Console.WriteLine("Title:\t{0}", issue.Title);
                Console.WriteLine("Date:\t{0}", issue.CreatedAt);
                Console.WriteLine("Body: \r\n{0}", issue.Body);
                Console.WriteLine("User:\t{0}", issue.User.Login);
                Console.WriteLine("--------");

                // ユーザーのアバター画像を取得
                var avatorUrl = new Uri(issue.User.AvatarUrl);
                var client = new WebClient();
                string downloadPath = @"d:\temp\" + issue.User.Login + ".png";
                await
                    client.DownloadFileTaskAsync(avatorUrl, downloadPath)
                        .ContinueWith(t => Console.WriteLine(downloadPath + " download Complte."));
            }
        }

        /// <summary>
        /// レポジトリのLabelを取得する
        /// </summary>
        /// <param name="password">
        /// The Password
        /// </param>
        public async void GetLabels(string password)
        {
            Console.WriteLine("レポジトリのLabelを取得する");
            var credential = new Credentials("ishisaka", password);
            var github = new GitHubClient(
                new ProductHeaderValue("IshisakaSample"), 
                new InMemoryCredentialStore(credential));

            var labels = github.Issue.Labels;
            foreach (var label in await labels.GetForRepository("ishisaka", "juzshizuoka"))
            {
                Console.WriteLine("Label:");
                Console.WriteLine("Name " + label.Name);
                Console.WriteLine("Color " + label.Color);
                Console.WriteLine("URL " + label.Url);
                Console.WriteLine("=========");
            }
        }

        /// <summary>
        /// レポジトリにIssueを追加・編集
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        public async void PutIssues(string password)
        {
            Console.WriteLine("レポジトリにIssueを追加する");
            var credential = new Credentials("ishisaka", password);
            var github = new GitHubClient(
                new ProductHeaderValue("IhsisakaSample"), 
                new InMemoryCredentialStore(credential));

            var newIssue = new NewIssue("サンプルイシュー" + DateTime.Now.ToShortTimeString())
                               {
                                   Body = "にほんごてきすと", 

                                   // 担当ユーザーの割り当てをする場合にはAssigneeプロパティに有効なユーザー名を指定
                                   Assignee = "ishisaka"
                               };

            // Issueを新規追加。追加されたIssueが戻ってくる
            var ret = await github.Issue.Create("ishisaka", "juzshizuoka", newIssue);
            Console.WriteLine("追加されたIssue");
            Console.WriteLine("Number:\t{0}", ret.Number);
            Console.WriteLine("Title:\t{0}", ret.Title);
            Console.WriteLine("Date:\t{0}", ret.CreatedAt);
            Console.WriteLine("Body: \r\n{0}", ret.Body);
            Console.WriteLine("User:\t{0}", ret.User.Login);
            Console.WriteLine("--------");

            // 編集
            // 編集 正直いちいちIssueUpdate作るのがメンドイ
            var issuesUpdate = new IssueUpdate { Title = ret.Title, Body = ret.Body + "\r\n編集しました。" };

            var retUpdate = await github.Issue.Update("ishisaka", "juzshizuoka", ret.Number, issuesUpdate);

            // 編集されたIssue
            Console.WriteLine("編集されたIssue");
            Console.WriteLine("Number:\t{0}", retUpdate.Number);
            Console.WriteLine("Title:\t{0}", retUpdate.Title);
            Console.WriteLine("Date:\t{0}", retUpdate.CreatedAt);
            Console.WriteLine("Body: \r\n{0}", retUpdate.Body);
            Console.WriteLine("User:\t{0}", retUpdate.User.Login);
            Console.WriteLine("--------");
        }

        #endregion
    }
}