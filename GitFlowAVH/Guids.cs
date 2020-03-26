using System;

namespace GitFlowAVH
{
    static class GuidList
    {
        public const string GuidGitFlowPkgString = "19ffe08d-8c71-4125-a024-6296bd27fce8";
        public const string GuidGitFlowCmdSetString = "9be3ffde-c8d0-4d89-824b-f310e4d05ca1";

        public static readonly Guid GuidGitFlowCmdSet = new Guid(GuidGitFlowCmdSetString);

        public const string GitFlowPage = "1F84BD1D-34B7-4745-BF5E-146299305344";
        public const string GitFlowActionSection = "4616C403-9D53-4A32-AB06-C067FB6079AA";
        public const string GitFlowFeaturesSection = "7DF0A7F3-C414-4573-AA7F-A8FFFF3722B8";
        public const string GitFlowBugfixesSection = "B5A25E96-71F7-41C8-BA6A-DDEAFEC6648F";
        public const string GitFlowInitSection = "321758C3-1B39-43E2-B82C-B62D0C9D6770";
        public const string GitFlowInstallSection = "2199A093-6D9E-4F50-9F61-357F18077CBB";
		public const string GitFlowReleasesSection = "9D09D120-8860-4944-8B92-6D59659072BA";
        public const string GitFlowHotfixesSection = "674CD3D5-52D8-4B45-983B-FF50F0C0E96A";
        public const string GitFlowSupportsSection = "627463AE-C324-44F3-AB08-8E64D255BE40";
    };
}