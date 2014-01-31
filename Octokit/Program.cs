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
            Console.Write("GitHubのパスワードを入力してください:  ");
            string passWord = Console.ReadLine();            
            var sample = new Sample();
            ////sample.GetIssues();
            sample.AuthenticationSample(passWord);
            sample.PutIssues(passWord);
            Console.WriteLine("終わったら Enter key を押してね.");
            Console.ReadLine();
        }

        #endregion
    }
}