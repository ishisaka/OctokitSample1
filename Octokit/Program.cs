﻿// --------------------------------------------------------------------------------------------------------------------
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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     実行ポイント
    /// </summary>
    public class Program
    {
        #region Methods

        /// <summary>
        ///     メインメソッド
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private static void Main()
        {
            Console.Write("GitHubのパスワードを入力してください:  ");
            var passWord = Console.ReadLine(); 
           
            // Octokit.NETでのサンプル
            var sample = new Sample();
            sample.GetIssues();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            sample.AuthenticationSample(passWord);
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            sample.PutIssues(passWord);
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine();
            sample.GetLabels(passWord);
            System.Threading.Thread.Sleep(2000);

            // Octokit.Reactiveでのサンプル
            Console.WriteLine("\r\nRxでのサンプル");

            var rxSample = new ReactiveSample();
            rxSample.GetIssues();
            System.Threading.Thread.Sleep(1000);
            rxSample.PutIssue(passWord);
            System.Threading.Thread.Sleep(1500);

            Console.WriteLine("終わったら Enter key を押してね.");
            Console.ReadLine();
        }

        #endregion
    }
}