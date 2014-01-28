// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Tadahiro Ishisaka" file="Program.cs">
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
// --------------------------------------------------------------------------------------------------------------------
namespace OctokitSample
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Headers;

    using Octokit;

    /// <summary>
    ///     実行ポイント
    /// </summary>
    public class Program
    {
        #region Methods

        /// <summary>
        ///     メインメソッド
        /// </summary>
        private static void Main()
        {
            var sample = new Sample();
            sample.GetIssues();
            Console.ReadLine();
        }

        #endregion
    }

    /// <summary>
    ///     OctoKit.NETを使用したGitHubアクセスのサンプル
    /// </summary>
    public class Sample
    {
        #region Public Methods and Operators

        /// <summary>
        ///     プロジェクトのIssueを取得するサンプル
        /// </summary>
        public async void GetIssues()
        {
            // クライアントを作成する。公開プロジェクトなら認証は必要なし
            var github = new GitHubClient(new ProductHeaderValue("IshisakaSample"));

            // イシューを取得する。オーナー名、プロジェクト名を引数に
            IReadOnlyList<Issue> issues = await github.Issue.GetForRepository("ishisaka", "nodeintellisense");
            foreach (Issue issue in issues)
            {
                Console.WriteLine("Number:\t{0}", issue.Number);
                Console.WriteLine("Title:\t{0}", issue.Title);
                Console.WriteLine("Date:\t{0}", issue.CreatedAt);
                Console.WriteLine("Body: \r\n{0}", issue.Body);
                Console.WriteLine("User:\t{0}", issue.User.Login);
                Console.WriteLine("--------");
                var avatorUrl = new Uri(issue.User.AvatarUrl);
                var client = new WebClient();
                string downloadPath = @"d:\temp\" + issue.User.Login + ".png";
                await
                    client.DownloadFileTaskAsync(avatorUrl, downloadPath)
                        .ContinueWith(t => Console.WriteLine(downloadPath + " download Complte."));
            }
        }

        #endregion
    }
}