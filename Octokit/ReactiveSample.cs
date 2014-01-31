// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReactiveSample.cs" company="Tadahiro Ishisaka">
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
//   Octokit.Reactiveを使用した場合のサンプル。
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace OctokitSample
{
    using System;
    using System.Net.Http.Headers;

    using Octokit;
    using Octokit.Internal;
    using Octokit.Reactive;

    /// <summary>
    /// Octokit.Reactiveを使用した場合のサンプル。
    /// </summary>
    public class ReactiveSample
    {
        #region Public Methods and Operators

        /// <summary>
        /// Rxを使ってレポジトリのIssueを取得する
        /// </summary>
        public void GetIssues()
        {
            // クライアントを作成する。公開レポジトリなら認証は必要なし
            var github = new GitHubClient(new ProductHeaderValue("IshisakaSample"));

            // ObservableIssuesClientを作成する
            var issueObserver = new ObservableIssuesClient(github);

            // レポジトリのIssueを取得して、表示する。Rxを使えばこんなに簡潔に書けます。
            issueObserver.GetForRepository("ishisaka", "nodeintellisense").Subscribe(
                i =>
                    {
                        Console.WriteLine("Number:\t{0}", i.Number);
                        Console.WriteLine("Title:\t{0}", i.Title);
                        Console.WriteLine("Date:\t{0}", i.CreatedAt);
                        Console.WriteLine("Body: \r\n{0}", i.Body);
                        Console.WriteLine("User:\t{0}", i.User.Login);
                        Console.WriteLine("--------");
                    });
        }

        /// <summary>
        /// レポジトリにIssueを追加・編集
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        public void PutIssue(string password)
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

            var issueObserver = new ObservableIssuesClient(github);
            issueObserver.Create("ishisaka", "juzshizuoka", newIssue).Subscribe(
                ret =>
                    {
                        Console.WriteLine("追加されたIssue");
                        Console.WriteLine("Number:\t{0}", ret.Number);
                        Console.WriteLine("Title:\t{0}", ret.Title);
                        Console.WriteLine("Date:\t{0}", ret.CreatedAt);
                        Console.WriteLine("Body: \r\n{0}", ret.Body);
                        Console.WriteLine("User:\t{0}", ret.User.Login);
                        Console.WriteLine("--------");
                    });
        }

        #endregion
    }
}