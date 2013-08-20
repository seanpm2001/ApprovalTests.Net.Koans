﻿using System;
using System.Diagnostics;
using System.IO;
using ApprovalTestKoans.Helpers;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Approvals = ApprovalTests.Approvals;

namespace ApprovalTestKoans.Lesson04
{
	[TestClass]
	[UseReporter(typeof (MsTestReporter))]
	public class ConfiguringReporters : Koans
	{
		[TestMethod]
		[UseReporter(typeof (_____))]
		public void ConfiguringTheFileLauncherReporterAtTheMethodLevel()
		{
			var reporter = Approvals.GetReporter();
			Assert.IsInstanceOfType(reporter, typeof (FileLauncherReporter));
		}

		[TestMethod]
		public void ConfiguringAClassLevelDefault()
		{
			var reporter = Approvals.GetReporter();
			Assert.IsInstanceOfType(reporter, typeof (_____));
		}
	}

	[TestClass]
	public class ConfiguringReportersPart2 : Koans
	{
		[TestMethod]
		public void ConfiguringAnAssemblyLevelDefault()
		{
			var reporter = Approvals.GetReporter();
			Assert.IsInstanceOfType(reporter, typeof (DiffReporter));
			// Hint: The blank for this problem is in the AssemblyInfo.cs file
		}
	}

	[TestClass]
	public class UsingReporters : Koans
	{
		[TestMethod]
		[UseReporter(typeof (ClipboardReporter), typeof (ImageWebReporter), typeof (TortoiseImageDiffReporter))]
		public void ConfiguringMultipleReporters()
		{
			var reporter = Approvals.GetReporter();
			MultiReporter multi = (MultiReporter) reporter;
			//var second = multi.Reporters[1];
			Assert.IsInstanceOfType(reporter, typeof (_____));
		}

//  [TestMethod]
// [UseReporter(typeof(FileLauncherReporter))]
//  public void UsingTextReportersForInsight() 
//  {
//    if (!decode("=fikpKnf").equals(____))
//    {
//      var  reporter = ReporterFactory.getFromAnnotation();
//      reporter.report(getPath("Insight.txt"), "");
//      Assert.fail("Please fill in the blank");
//    }
//  }
//  [TestMethod]
// [UseReporter(typeof(ImageWebReporter))]
//  public void UsingImageForInsight() 
//  {
//    String whatWasTheOldColor = ___;
//    if (!decode("Ycl\\").equals(whatWasTheOldColor.toLowerCase()))
//    {
//      var  reporter = ReporterFactory.getFromAnnotation();
//      reporter.report(getPath("NewImage.png"), getPath("OldImage.png"));
//      Assert.fail("Please fill in the blank");
//    }
//  }
	}

	public class _____ : IApprovalFailureReporter
	{
		public void Report(string approved, string received)
		{
			throw new NotImplementedException();
		}
	}

	public class ImageWebReporter : IEnvironmentAwareReporter
	{
		public static ImageWebReporter INSTANCE = new ImageWebReporter();

		public void Report(string approved, string received)
		{
			String text =
				"<html><body><center><table border=1><tr><td><img src=\"file:///{0}\"></td><td><img src=\"file:///{1}\"></td></tr><tr><td>approved</td><td>received</td></tr></table>  {2} <br/> <b>to approve :</b> copy clipboard to command window  <br/> <font size=1>{3}</font></center></body></html>";
			String moveText = QuietReporter.GetCommandLineForApproval(approved, received);
			text = text.FormatWith(approved, received, received, moveText);
			string file = Path.GetTempFileName() + ".html";
			File.WriteAllText(file, text);
			Process.Start(file);
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return GenericDiffReporter.IsFileOneOf(forFile, GenericDiffReporter.IMAGE_FILE_TYPES);
		}
	}
}