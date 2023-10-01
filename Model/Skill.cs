using System;
using System.Collections.Generic;

namespace D2SLib2.Model;

public partial class Skill
{
    public long Id { get; set; }

    public string? Skill1 { get; set; }

    public string? Charclass { get; set; }

    public string? Skilldesc { get; set; }

    public double? Srvstfunc { get; set; }

    public double? Srvdofunc { get; set; }

    public double? Srvstopfunc { get; set; }

    public double? Prgstack { get; set; }

    public double? Srvprgfunc1 { get; set; }

    public double? Srvprgfunc2 { get; set; }

    public double? Srvprgfunc3 { get; set; }

    public double? Prgcalc1 { get; set; }

    public double? Prgcalc2 { get; set; }

    public double? Prgcalc3 { get; set; }

    public double? Prgdam { get; set; }

    public string? Srvmissile { get; set; }

    public double? Decquant { get; set; }

    public double? Lob { get; set; }

    public string? Srvmissilea { get; set; }

    public string? Srvmissileb { get; set; }

    public string? Srvmissilec { get; set; }

    public double? UseServerMissilesOnRemoteClients { get; set; }

    public double? Srvoverlay { get; set; }

    public double? Aurafilter { get; set; }

    public string? Aurastate { get; set; }

    public string? Auratargetstate { get; set; }

    public string? Auralencalc { get; set; }

    public string? Aurarangecalc { get; set; }

    public string? Aurastat1 { get; set; }

    public string? Aurastatcalc1 { get; set; }

    public string? Aurastat2 { get; set; }

    public string? Aurastatcalc2 { get; set; }

    public string? Aurastat3 { get; set; }

    public string? Aurastatcalc3 { get; set; }

    public string? Aurastat4 { get; set; }

    public string? Aurastatcalc4 { get; set; }

    public string? Aurastat5 { get; set; }

    public string? Aurastatcalc5 { get; set; }

    public string? Aurastat6 { get; set; }

    public string? Aurastatcalc6 { get; set; }

    public string? Auraevent1 { get; set; }

    public double? Auraeventfunc1 { get; set; }

    public string? Auraevent2 { get; set; }

    public double? Auraeventfunc2 { get; set; }

    public string? Auraevent3 { get; set; }

    public double? Auraeventfunc3 { get; set; }

    public string? Passivestate { get; set; }

    public double? Passiveitype { get; set; }

    public double? Passivereqweaponcount { get; set; }

    public string? Passivestat1 { get; set; }

    public string? Passivecalc1 { get; set; }

    public string? Passivestat2 { get; set; }

    public string? Passivecalc2 { get; set; }

    public string? Passivestat3 { get; set; }

    public string? Passivecalc3 { get; set; }

    public string? Passivestat4 { get; set; }

    public string? Passivecalc4 { get; set; }

    public string? Passivestat5 { get; set; }

    public string? Passivecalc5 { get; set; }

    public string? Passivestat6 { get; set; }

    public string? Passivecalc6 { get; set; }

    public string? Passivestat7 { get; set; }

    public string? Passivecalc7 { get; set; }

    public string? Passivestat8 { get; set; }

    public string? Passivecalc8 { get; set; }

    public string? Passivestat9 { get; set; }

    public string? Passivecalc9 { get; set; }

    public string? Passivestat10 { get; set; }

    public string? Passivecalc10 { get; set; }

    public double? Passivestat11 { get; set; }

    public double? Passivecalc11 { get; set; }

    public double? Passivestat12 { get; set; }

    public double? Passivecalc12 { get; set; }

    public double? Passivestat13 { get; set; }

    public double? Passivecalc13 { get; set; }

    public double? Passivestat14 { get; set; }

    public double? Passivecalc14 { get; set; }

    public string? Summon { get; set; }

    public string? Pettype { get; set; }

    public string? Petmax { get; set; }

    public string? Summode { get; set; }

    public string? Sumskill1 { get; set; }

    public string? Sumsk1calc { get; set; }

    public string? Sumskill2 { get; set; }

    public string? Sumsk2calc { get; set; }

    public string? Sumskill3 { get; set; }

    public string? Sumsk3calc { get; set; }

    public string? Sumskill4 { get; set; }

    public string? Sumsk4calc { get; set; }

    public double? Sumskill5 { get; set; }

    public double? Sumsk5calc { get; set; }

    public double? Sumumod { get; set; }

    public string? Sumoverlay { get; set; }

    public double? Stsuccessonly { get; set; }

    public string? Stsound { get; set; }

    public string? Stsoundclass { get; set; }

    public double? Stsounddelay { get; set; }

    public double? Weaponsnd { get; set; }

    public string? Dosound { get; set; }

    public string? Dosounda { get; set; }

    public string? Dosoundb { get; set; }

    public double? Tgtoverlay { get; set; }

    public string? Tgtsound { get; set; }

    public string? Prgoverlay { get; set; }

    public double? Prgsound { get; set; }

    public string? Castoverlay { get; set; }

    public string? Cltoverlaya { get; set; }

    public double? Cltoverlayb { get; set; }

    public double? Cltstfunc { get; set; }

    public string? Cltdofunc { get; set; }

    public double? Cltstopfunc { get; set; }

    public double? Cltprgfunc1 { get; set; }

    public double? Cltprgfunc2 { get; set; }

    public double? Cltprgfunc3 { get; set; }

    public string? Cltmissile { get; set; }

    public string? Cltmissilea { get; set; }

    public string? Cltmissileb { get; set; }

    public string? Cltmissilec { get; set; }

    public double? Cltmissiled { get; set; }

    public double? Cltcalc1 { get; set; }

    public string? Cltcalc1desc { get; set; }

    public double? Cltcalc2 { get; set; }

    public string? Cltcalc2desc { get; set; }

    public double? Cltcalc3 { get; set; }

    public string? Cltcalc3desc { get; set; }

    public double? Warp { get; set; }

    public double? Immediate { get; set; }

    public double? Enhanceable { get; set; }

    public long? Attackrank { get; set; }

    public double? Noammo { get; set; }

    public string? Range { get; set; }

    public double? Weapsel { get; set; }

    public string? Itypea1 { get; set; }

    public string? Itypea2 { get; set; }

    public double? Itypea3 { get; set; }

    public string? Etypea1 { get; set; }

    public double? Etypea2 { get; set; }

    public double? Itypeb1 { get; set; }

    public double? Itypeb2 { get; set; }

    public double? Itypeb3 { get; set; }

    public double? Etypeb1 { get; set; }

    public double? Etypeb2 { get; set; }

    public string? Anim { get; set; }

    public string? Seqtrans { get; set; }

    public string? Monanim { get; set; }

    public double? Seqnum { get; set; }

    public double? Seqinput { get; set; }

    public double? Durability { get; set; }

    public double? UseAttackRate { get; set; }

    public double? LineOfSight { get; set; }

    public double? TargetableOnly { get; set; }

    public double? SearchEnemyXy { get; set; }

    public double? SearchEnemyNear { get; set; }

    public double? SearchOpenXy { get; set; }

    public double? SelectProc { get; set; }

    public double? TargetCorpse { get; set; }

    public double? TargetPet { get; set; }

    public double? TargetAlly { get; set; }

    public double? TargetItem { get; set; }

    public double? AttackNoMana { get; set; }

    public double? TgtPlaceCheck { get; set; }

    public double? KeepCursorStateOnKill { get; set; }

    public double? ContinueCastUnselected { get; set; }

    public double? ClearSelectedOnHold { get; set; }

    public double? ItemEffect { get; set; }

    public double? ItemCltEffect { get; set; }

    public double? ItemTgtDo { get; set; }

    public double? ItemTarget { get; set; }

    public double? ItemUseRestrict { get; set; }

    public double? ItemCheckStart { get; set; }

    public double? ItemCltCheckStart { get; set; }

    public string? ItemCastSound { get; set; }

    public string? ItemCastOverlay { get; set; }

    public double? Skpoints { get; set; }

    public long? Reqlevel { get; set; }

    public double? Maxlvl { get; set; }

    public double? Reqstr { get; set; }

    public double? Reqdex { get; set; }

    public double? Reqint { get; set; }

    public double? Reqvit { get; set; }

    public string? Reqskill1 { get; set; }

    public string? Reqskill2 { get; set; }

    public double? Reqskill3 { get; set; }

    public double? Restrict { get; set; }

    public double? State1 { get; set; }

    public double? State2 { get; set; }

    public double? State3 { get; set; }

    public double? Localdelay { get; set; }

    public double? Globaldelay { get; set; }

    public long? Leftskill { get; set; }

    public long? Rightskill { get; set; }

    public double? Repeat { get; set; }

    public double? Alwayshit { get; set; }

    public double? Usemanaondo { get; set; }

    public double? Startmana { get; set; }

    public long? Minmana { get; set; }

    public long? Manashift { get; set; }

    public long? Mana { get; set; }

    public long? Lvlmana { get; set; }

    public double? Interrupt { get; set; }

    public double? InTown { get; set; }

    public double? Aura { get; set; }

    public double? Periodic { get; set; }

    public string? Perdelay { get; set; }

    public double? Finishing { get; set; }

    public double? Prgchargestocast { get; set; }

    public double? Prgchargesconsumed { get; set; }

    public double? Passive { get; set; }

    public double? Progressive { get; set; }

    public double? Scroll { get; set; }

    public string? Calc1 { get; set; }

    public string? Calc1desc { get; set; }

    public string? Calc2 { get; set; }

    public string? Calc2desc { get; set; }

    public string? Calc3 { get; set; }

    public string? Calc3desc { get; set; }

    public double? Calc4 { get; set; }

    public string? Calc4desc { get; set; }

    public string? Calc5 { get; set; }

    public string? Calc5desc { get; set; }

    public double? Calc6 { get; set; }

    public double? Calc6desc { get; set; }

    public double? Param1 { get; set; }

    public string? Param1Description { get; set; }

    public double? Param2 { get; set; }

    public string? Param2Description { get; set; }

    public double? Param3 { get; set; }

    public string? Param3Description { get; set; }

    public double? Param4 { get; set; }

    public string? Param4Description { get; set; }

    public double? Param5 { get; set; }

    public string? Param5Description { get; set; }

    public double? Param6 { get; set; }

    public string? Param6Description { get; set; }

    public double? Param7 { get; set; }

    public string? Param7Description { get; set; }

    public double? Param8 { get; set; }

    public string? Param8Description { get; set; }

    public double? Param9 { get; set; }

    public string? Param9Description { get; set; }

    public double? Param10 { get; set; }

    public double? Param10Description2 { get; set; }

    public double? Param11 { get; set; }

    public double? Param11Description { get; set; }

    public double? Param12 { get; set; }

    public double? Param12Description { get; set; }

    public long? InGame { get; set; }

    public double? ToHit { get; set; }

    public double? LevToHit { get; set; }

    public string? ToHitCalc { get; set; }

    public double? ResultFlags { get; set; }

    public double? HitFlags { get; set; }

    public double? HitClass { get; set; }

    public double? Kick { get; set; }

    public long? HitShift { get; set; }

    public double? SrcDam { get; set; }

    public double? MinDam { get; set; }

    public double? MinLevDam1 { get; set; }

    public double? MinLevDam2 { get; set; }

    public double? MinLevDam3 { get; set; }

    public double? MinLevDam4 { get; set; }

    public double? MinLevDam5 { get; set; }

    public double? MaxDam { get; set; }

    public double? MaxLevDam1 { get; set; }

    public double? MaxLevDam2 { get; set; }

    public double? MaxLevDam3 { get; set; }

    public double? MaxLevDam4 { get; set; }

    public double? MaxLevDam5 { get; set; }

    public double? DmgSymPerCalc { get; set; }

    public string? Etype { get; set; }

    public double? Emin { get; set; }

    public double? EminLev1 { get; set; }

    public double? EminLev2 { get; set; }

    public double? EminLev3 { get; set; }

    public double? EminLev4 { get; set; }

    public double? EminLev5 { get; set; }

    public double? Emax { get; set; }

    public double? EmaxLev1 { get; set; }

    public double? EmaxLev2 { get; set; }

    public double? EmaxLev3 { get; set; }

    public double? EmaxLev4 { get; set; }

    public double? EmaxLev5 { get; set; }

    public string? EdmgSymPerCalc { get; set; }

    public double? Elen { get; set; }

    public double? ElevLen1 { get; set; }

    public double? ElevLen2 { get; set; }

    public double? ElevLen3 { get; set; }

    public string? ElenSymPerCalc { get; set; }

    public double? Aitype { get; set; }

    public double? Aibonus { get; set; }

    public double? Costmult { get; set; }

    public long? Costadd { get; set; }

    public long? Eol { get; set; }
}
