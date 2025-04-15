using System;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000083 RID: 131
	internal enum EventCodes : byte
	{
		// Token: 0x0400017C RID: 380
		VoiceData = 1,
		// Token: 0x0400017D RID: 381
		ServerMessage,
		// Token: 0x0400017E RID: 382
		MasterClientSync = 4,
		// Token: 0x0400017F RID: 383
		CachedEvent,
		// Token: 0x04000180 RID: 384
		MasterClientSyncFinished,
		// Token: 0x04000181 RID: 385
		VRChatRPC,
		// Token: 0x04000182 RID: 386
		SerializedData,
		// Token: 0x04000183 RID: 387
		InterestManagement,
		// Token: 0x04000184 RID: 388
		SerializedDataReliable,
		// Token: 0x04000185 RID: 389
		Unknown10,
		// Token: 0x04000186 RID: 390
		Unknown11,
		// Token: 0x04000187 RID: 391
		SerializedDataUnreliable,
		// Token: 0x04000188 RID: 392
		ReliableFlatbuffer,
		// Token: 0x04000189 RID: 393
		ObjectSync = 16,
		// Token: 0x0400018A RID: 394
		Unknown17,
		// Token: 0x0400018B RID: 395
		JoinWorld = 20,
		// Token: 0x0400018C RID: 396
		ObjectOwnership = 22,
		// Token: 0x0400018D RID: 397
		Moderations = 33,
		// Token: 0x0400018E RID: 398
		PhotonEventLimits,
		// Token: 0x0400018F RID: 399
		PhotonHeartbeat,
		// Token: 0x04000190 RID: 400
		AvatarRefresh = 40,
		// Token: 0x04000191 RID: 401
		SetPlayerData = 42,
		// Token: 0x04000192 RID: 402
		Unknown43,
		// Token: 0x04000193 RID: 403
		OwnChatbox,
		// Token: 0x04000194 RID: 404
		OnLoaded = 51,
		// Token: 0x04000195 RID: 405
		PlayerReady = 53,
		// Token: 0x04000196 RID: 406
		PhysBonesPermissions = 60,
		// Token: 0x04000197 RID: 407
		EACHeartBeat = 66,
		// Token: 0x04000198 RID: 408
		Portal = 70,
		// Token: 0x04000199 RID: 409
		EmojiSend = 73,
		// Token: 0x0400019A RID: 410
		PhotonRPC = 200,
		// Token: 0x0400019B RID: 411
		SendSerialize,
		// Token: 0x0400019C RID: 412
		Instantiate,
		// Token: 0x0400019D RID: 413
		CloseConnection,
		// Token: 0x0400019E RID: 414
		Destroy,
		// Token: 0x0400019F RID: 415
		RemoveCachedRPCs,
		// Token: 0x040001A0 RID: 416
		SendSerializeReliable,
		// Token: 0x040001A1 RID: 417
		DestroyPlayer,
		// Token: 0x040001A2 RID: 418
		AssignMasterClient,
		// Token: 0x040001A3 RID: 419
		OwnershipRequest,
		// Token: 0x040001A4 RID: 420
		OwnershipTransfer,
		// Token: 0x040001A5 RID: 421
		VacantViewIds,
		// Token: 0x040001A6 RID: 422
		LevelReload,
		// Token: 0x040001A7 RID: 423
		PhotonAppId = 220,
		// Token: 0x040001A8 RID: 424
		AuthEvent = 223,
		// Token: 0x040001A9 RID: 425
		LobbyStats,
		// Token: 0x040001AA RID: 426
		AppStats = 226,
		// Token: 0x040001AB RID: 427
		Match,
		// Token: 0x040001AC RID: 428
		QueueState,
		// Token: 0x040001AD RID: 429
		GameListUpdate,
		// Token: 0x040001AE RID: 430
		GameList,
		// Token: 0x040001AF RID: 431
		CacheSliceChanged = 250,
		// Token: 0x040001B0 RID: 432
		ErrorInfo,
		// Token: 0x040001B1 RID: 433
		SetProperties = 253,
		// Token: 0x040001B2 RID: 434
		LeavePhoton,
		// Token: 0x040001B3 RID: 435
		JoinPhoton
	}
}
