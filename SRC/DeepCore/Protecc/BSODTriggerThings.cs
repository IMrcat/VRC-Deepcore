using System;
using System.Runtime.InteropServices;

namespace DeepCore.Protecc
{
	// Token: 0x02000009 RID: 9
	internal class BSODTriggerThings
	{
		// Token: 0x06000032 RID: 50
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern IntPtr RtlAdjustPrivilege(BSODTriggerThings.Privilege privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

		// Token: 0x06000033 RID: 51
		[DllImport("ntdll.dll")]
		public static extern uint NtRaiseHardError(BSODTriggerThings.NTStatus ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

		// Token: 0x06000034 RID: 52 RVA: 0x0000348C File Offset: 0x0000168C
		public static void PCCrash()
		{
			bool flag;
			BSODTriggerThings.RtlAdjustPrivilege(BSODTriggerThings.Privilege.SeShutdownPrivilege, true, false, out flag);
			uint num;
			BSODTriggerThings.NtRaiseHardError((BSODTriggerThings.NTStatus)3221226528U, 0U, 0U, IntPtr.Zero, 6U, out num);
		}

		// Token: 0x020000AE RID: 174
		public enum Privilege
		{
			// Token: 0x0400023A RID: 570
			SeCreateTokenPrivilege = 1,
			// Token: 0x0400023B RID: 571
			SeAssignPrimaryTokenPrivilege,
			// Token: 0x0400023C RID: 572
			SeLockMemoryPrivilege,
			// Token: 0x0400023D RID: 573
			SeIncreaseQuotaPrivilege,
			// Token: 0x0400023E RID: 574
			SeUnsolicitedInputPrivilege,
			// Token: 0x0400023F RID: 575
			SeMachineAccountPrivilege,
			// Token: 0x04000240 RID: 576
			SeTcbPrivilege,
			// Token: 0x04000241 RID: 577
			SeSecurityPrivilege,
			// Token: 0x04000242 RID: 578
			SeTakeOwnershipPrivilege,
			// Token: 0x04000243 RID: 579
			SeLoadDriverPrivilege,
			// Token: 0x04000244 RID: 580
			SeSystemProfilePrivilege,
			// Token: 0x04000245 RID: 581
			SeSystemtimePrivilege,
			// Token: 0x04000246 RID: 582
			SeProfileSingleProcessPrivilege,
			// Token: 0x04000247 RID: 583
			SeIncreaseBasePriorityPrivilege,
			// Token: 0x04000248 RID: 584
			SeCreatePagefilePrivilege,
			// Token: 0x04000249 RID: 585
			SeCreatePermanentPrivilege,
			// Token: 0x0400024A RID: 586
			SeBackupPrivilege,
			// Token: 0x0400024B RID: 587
			SeRestorePrivilege,
			// Token: 0x0400024C RID: 588
			SeShutdownPrivilege,
			// Token: 0x0400024D RID: 589
			SeDebugPrivilege,
			// Token: 0x0400024E RID: 590
			SeAuditPrivilege,
			// Token: 0x0400024F RID: 591
			SeSystemEnvironmentPrivilege,
			// Token: 0x04000250 RID: 592
			SeChangeNotifyPrivilege,
			// Token: 0x04000251 RID: 593
			SeRemoteShutdownPrivilege,
			// Token: 0x04000252 RID: 594
			SeUndockPrivilege,
			// Token: 0x04000253 RID: 595
			SeSyncAgentPrivilege,
			// Token: 0x04000254 RID: 596
			SeEnableDelegationPrivilege,
			// Token: 0x04000255 RID: 597
			SeManageVolumePrivilege,
			// Token: 0x04000256 RID: 598
			SeImpersonatePrivilege,
			// Token: 0x04000257 RID: 599
			SeCreateGlobalPrivilege,
			// Token: 0x04000258 RID: 600
			SeTrustedCredManAccessPrivilege,
			// Token: 0x04000259 RID: 601
			SeRelabelPrivilege,
			// Token: 0x0400025A RID: 602
			SeIncreaseWorkingSetPrivilege,
			// Token: 0x0400025B RID: 603
			SeTimeZonePrivilege,
			// Token: 0x0400025C RID: 604
			SeCreateSymbolicLinkPrivilege
		}

		// Token: 0x020000AF RID: 175
		public enum NTStatus : uint
		{
			// Token: 0x0400025E RID: 606
			STATUS_SUCCESS,
			// Token: 0x0400025F RID: 607
			STATUS_WAIT_0 = 0U,
			// Token: 0x04000260 RID: 608
			STATUS_WAIT_1,
			// Token: 0x04000261 RID: 609
			STATUS_WAIT_2,
			// Token: 0x04000262 RID: 610
			STATUS_WAIT_3,
			// Token: 0x04000263 RID: 611
			STATUS_WAIT_63 = 63U,
			// Token: 0x04000264 RID: 612
			STATUS_ABANDONED = 128U,
			// Token: 0x04000265 RID: 613
			STATUS_ABANDONED_WAIT_0 = 128U,
			// Token: 0x04000266 RID: 614
			STATUS_ABANDONED_WAIT_63 = 191U,
			// Token: 0x04000267 RID: 615
			STATUS_USER_APC,
			// Token: 0x04000268 RID: 616
			STATUS_KERNEL_APC = 256U,
			// Token: 0x04000269 RID: 617
			STATUS_ALERTED,
			// Token: 0x0400026A RID: 618
			STATUS_TIMEOUT,
			// Token: 0x0400026B RID: 619
			STATUS_PENDING,
			// Token: 0x0400026C RID: 620
			STATUS_REPARSE,
			// Token: 0x0400026D RID: 621
			STATUS_CRASH_DUMP = 278U,
			// Token: 0x0400026E RID: 622
			DBG_EXCEPTION_HANDLED = 65537U,
			// Token: 0x0400026F RID: 623
			DBG_CONTINUE,
			// Token: 0x04000270 RID: 624
			STATUS_PRIVILEGED_INSTRUCTION = 3221225622U,
			// Token: 0x04000271 RID: 625
			STATUS_MEMORY_NOT_ALLOCATED = 3221225632U,
			// Token: 0x04000272 RID: 626
			STATUS_BIOS_FAILED_TO_CONNECT_INTERRUPT = 3221225838U,
			// Token: 0x04000273 RID: 627
			STATUS_ASSERTION_FAILURE = 3221226528U
		}
	}
}
