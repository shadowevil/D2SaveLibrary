﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace D2SLib2.Model;

public partial class D2slibDataContext : DbContext
{
    public D2slibDataContext()
    {
    }

    public D2slibDataContext(DbContextOptions<D2slibDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ItemStatCost> ItemStatCosts { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<ItemsTemplate> ItemsTemplates { get; set; }

    public virtual DbSet<Playerclass> Playerclasses { get; set; }

    public virtual DbSet<Rune> Runes { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=C:\\Users\\ShadowEvil\\source\\repos\\D2SLib2\\D2STesting\\D2SLibData.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemStatCost>(entity =>
        {
            entity.ToTable("ItemStatCost");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Advdisplay).HasColumnName("advdisplay");
            entity.Property(e => e.CsvBits).HasColumnName("CSvBits");
            entity.Property(e => e.CsvParam).HasColumnName("CSvParam");
            entity.Property(e => e.CsvSigned).HasColumnName("CSvSigned");
            entity.Property(e => e.Damagerelated).HasColumnName("damagerelated");
            entity.Property(e => e.Descfunc).HasColumnName("descfunc");
            entity.Property(e => e.Descpriority).HasColumnName("descpriority");
            entity.Property(e => e.Descstr2).HasColumnName("descstr2");
            entity.Property(e => e.Descstrneg).HasColumnName("descstrneg");
            entity.Property(e => e.Descstrpos).HasColumnName("descstrpos");
            entity.Property(e => e.Descval).HasColumnName("descval");
            entity.Property(e => e.Dgrp).HasColumnName("dgrp");
            entity.Property(e => e.Dgrpfunc).HasColumnName("dgrpfunc");
            entity.Property(e => e.Dgrpstr2).HasColumnName("dgrpstr2");
            entity.Property(e => e.Dgrpstrneg).HasColumnName("dgrpstrneg");
            entity.Property(e => e.Dgrpstrpos).HasColumnName("dgrpstrpos");
            entity.Property(e => e.Dgrpval).HasColumnName("dgrpval");
            entity.Property(e => e.Direct).HasColumnName("direct");
            entity.Property(e => e.Eol).HasColumnName("eol");
            entity.Property(e => e.FCallback).HasColumnName("fCallback");
            entity.Property(e => e.FMin).HasColumnName("fMin");
            entity.Property(e => e.Itemevent1).HasColumnName("itemevent1");
            entity.Property(e => e.Itemevent2).HasColumnName("itemevent2");
            entity.Property(e => e.Itemeventfunc1).HasColumnName("itemeventfunc1");
            entity.Property(e => e.Itemeventfunc2).HasColumnName("itemeventfunc2");
            entity.Property(e => e.Keepzero).HasColumnName("keepzero");
            entity.Property(e => e.Maxstat).HasColumnName("maxstat");
            entity.Property(e => e.Op).HasColumnName("op");
            entity.Property(e => e.Opbase).HasColumnName("opbase");
            entity.Property(e => e.Opparam).HasColumnName("opparam");
            entity.Property(e => e.Opstat1).HasColumnName("opstat1");
            entity.Property(e => e.Opstat2).HasColumnName("opstat2");
            entity.Property(e => e.Opstat3).HasColumnName("opstat3");
            entity.Property(e => e.Stuff).HasColumnName("stuff");
            entity.Property(e => e._109SaveAdd).HasColumnName("1.09-SaveAdd");
            entity.Property(e => e._109SaveBits).HasColumnName("1.09-SaveBits");
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Eol).HasColumnName("eol");
            entity.Property(e => e.ItemType1).HasColumnName("ItemType");
        });

        modelBuilder.Entity<ItemsTemplate>(entity =>
        {
            entity.ToTable("ItemsTemplate");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Absorbs).HasColumnName("absorbs");
            entity.Property(e => e.Advdisplay).HasColumnName("advdisplay");
            entity.Property(e => e.Alternategfx).HasColumnName("alternategfx");
            entity.Property(e => e.AutoPrefix).HasColumnName("auto_prefix");
            entity.Property(e => e.Autobelt).HasColumnName("autobelt");
            entity.Property(e => e.Belt).HasColumnName("belt");
            entity.Property(e => e.Bitfield1).HasColumnName("bitfield1");
            entity.Property(e => e.Block).HasColumnName("block");
            entity.Property(e => e.Calc1).HasColumnName("calc1");
            entity.Property(e => e.Calc2).HasColumnName("calc2");
            entity.Property(e => e.Calc3).HasColumnName("calc3");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Compactsave).HasColumnName("compactsave");
            entity.Property(e => e.Component).HasColumnName("component");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Cstate1).HasColumnName("cstate1");
            entity.Property(e => e.Cstate2).HasColumnName("cstate2");
            entity.Property(e => e.CsvBits).HasColumnName("CSvBits");
            entity.Property(e => e.CsvParam).HasColumnName("CSvParam");
            entity.Property(e => e.CsvSigned).HasColumnName("CSvSigned");
            entity.Property(e => e.Damagerelated).HasColumnName("damagerelated");
            entity.Property(e => e.Descfunc).HasColumnName("descfunc");
            entity.Property(e => e.Descpriority).HasColumnName("descpriority");
            entity.Property(e => e.Descstr2).HasColumnName("descstr2");
            entity.Property(e => e.Descstrneg).HasColumnName("descstrneg");
            entity.Property(e => e.Descstrpos).HasColumnName("descstrpos");
            entity.Property(e => e.Descval).HasColumnName("descval");
            entity.Property(e => e.Dgrp).HasColumnName("dgrp");
            entity.Property(e => e.Dgrpfunc).HasColumnName("dgrpfunc");
            entity.Property(e => e.Dgrpstr2).HasColumnName("dgrpstr2");
            entity.Property(e => e.Dgrpstrneg).HasColumnName("dgrpstrneg");
            entity.Property(e => e.Dgrpstrpos).HasColumnName("dgrpstrpos");
            entity.Property(e => e.Dgrpval).HasColumnName("dgrpval");
            entity.Property(e => e.Direct).HasColumnName("direct");
            entity.Property(e => e.Dropsfxframe).HasColumnName("dropsfxframe");
            entity.Property(e => e.Dropsound).HasColumnName("dropsound");
            entity.Property(e => e.Durability).HasColumnName("durability");
            entity.Property(e => e.Durwarning).HasColumnName("durwarning");
            entity.Property(e => e.Eol).HasColumnName("eol");
            entity.Property(e => e.FCallback).HasColumnName("fCallback");
            entity.Property(e => e.FMin).HasColumnName("fMin");
            entity.Property(e => e.Flippyfile).HasColumnName("flippyfile");
            entity.Property(e => e.GambleCost).HasColumnName("gamble_cost");
            entity.Property(e => e.Gemapplytype).HasColumnName("gemapplytype");
            entity.Property(e => e.Gemoffset).HasColumnName("gemoffset");
            entity.Property(e => e.Gemsockets).HasColumnName("gemsockets");
            entity.Property(e => e.Hasinv).HasColumnName("hasinv");
            entity.Property(e => e.Hitclass).HasColumnName("hitclass");
            entity.Property(e => e.Invfile).HasColumnName("invfile");
            entity.Property(e => e.Invheight).HasColumnName("invheight");
            entity.Property(e => e.Invwidth).HasColumnName("invwidth");
            entity.Property(e => e.Itemevent1).HasColumnName("itemevent1");
            entity.Property(e => e.Itemevent2).HasColumnName("itemevent2");
            entity.Property(e => e.Itemeventfunc1).HasColumnName("itemeventfunc1");
            entity.Property(e => e.Itemeventfunc2).HasColumnName("itemeventfunc2");
            entity.Property(e => e.Keepzero).HasColumnName("keepzero");
            entity.Property(e => e.LArm).HasColumnName("lArm");
            entity.Property(e => e.LSpad).HasColumnName("lSPad");
            entity.Property(e => e.Len).HasColumnName("len");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Levelreq).HasColumnName("levelreq");
            entity.Property(e => e.Lightradius).HasColumnName("lightradius");
            entity.Property(e => e.Magiclvl).HasColumnName("magiclvl");
            entity.Property(e => e.Maxac).HasColumnName("maxac");
            entity.Property(e => e.Maxdam).HasColumnName("maxdam");
            entity.Property(e => e.Maxdam1).HasColumnName("maxdam_1");
            entity.Property(e => e.Maxmisdam).HasColumnName("maxmisdam");
            entity.Property(e => e.Maxstack).HasColumnName("maxstack");
            entity.Property(e => e.Maxstat).HasColumnName("maxstat");
            entity.Property(e => e.Minac).HasColumnName("minac");
            entity.Property(e => e.Mindam).HasColumnName("mindam");
            entity.Property(e => e.Mindam1).HasColumnName("mindam_1");
            entity.Property(e => e.Minmisdam).HasColumnName("minmisdam");
            entity.Property(e => e.Minstack).HasColumnName("minstack");
            entity.Property(e => e.Missiletype).HasColumnName("missiletype");
            entity.Property(e => e.Multibuy).HasColumnName("multibuy");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namestr).HasColumnName("namestr");
            entity.Property(e => e.Nodurability).HasColumnName("nodurability");
            entity.Property(e => e.Normcode).HasColumnName("normcode");
            entity.Property(e => e.Op).HasColumnName("op");
            entity.Property(e => e.Opbase).HasColumnName("opbase");
            entity.Property(e => e.Opparam).HasColumnName("opparam");
            entity.Property(e => e.Opstat1).HasColumnName("opstat1");
            entity.Property(e => e.Opstat2).HasColumnName("opstat2");
            entity.Property(e => e.Opstat3).HasColumnName("opstat3");
            entity.Property(e => e.PSpell).HasColumnName("pSpell");
            entity.Property(e => e.Qntwarning).HasColumnName("qntwarning");
            entity.Property(e => e.Quest).HasColumnName("quest");
            entity.Property(e => e.Questdiffcheck).HasColumnName("questdiffcheck");
            entity.Property(e => e.Quivered).HasColumnName("quivered");
            entity.Property(e => e.RArm).HasColumnName("rArm");
            entity.Property(e => e.RSpad).HasColumnName("rSPad");
            entity.Property(e => e.Rangeadder).HasColumnName("rangeadder");
            entity.Property(e => e.Rarity).HasColumnName("rarity");
            entity.Property(e => e.Reqdex).HasColumnName("reqdex");
            entity.Property(e => e.Reqstr).HasColumnName("reqstr");
            entity.Property(e => e.Setinvfile).HasColumnName("setinvfile");
            entity.Property(e => e.Spawnable).HasColumnName("spawnable");
            entity.Property(e => e.Spawnstack).HasColumnName("spawnstack");
            entity.Property(e => e.Special).HasColumnName("special");
            entity.Property(e => e.Speed).HasColumnName("speed");
            entity.Property(e => e.Spelldesc).HasColumnName("spelldesc");
            entity.Property(e => e.Spelldesccalc).HasColumnName("spelldesccalc");
            entity.Property(e => e.Spelldescstr).HasColumnName("spelldescstr");
            entity.Property(e => e.Spellicon).HasColumnName("spellicon");
            entity.Property(e => e.Spelloffset).HasColumnName("spelloffset");
            entity.Property(e => e.Stackable).HasColumnName("stackable");
            entity.Property(e => e.Stat1).HasColumnName("stat1");
            entity.Property(e => e.Stat2).HasColumnName("stat2");
            entity.Property(e => e.Stat3).HasColumnName("stat3");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Stuff).HasColumnName("stuff");
            entity.Property(e => e.SzFlavorText).HasColumnName("szFlavorText");
            entity.Property(e => e.Throwable).HasColumnName("throwable");
            entity.Property(e => e.TmogMax).HasColumnName("TMogMax");
            entity.Property(e => e.TmogMin).HasColumnName("TMogMin");
            entity.Property(e => e.TmogType).HasColumnName("TMogType");
            entity.Property(e => e.Transparent).HasColumnName("transparent");
            entity.Property(e => e.Transtbl).HasColumnName("transtbl");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Type2).HasColumnName("type2");
            entity.Property(e => e.Ubercode).HasColumnName("ubercode");
            entity.Property(e => e.Ultracode).HasColumnName("ultracode");
            entity.Property(e => e.Unique).HasColumnName("unique");
            entity.Property(e => e.Uniqueinvfile).HasColumnName("uniqueinvfile");
            entity.Property(e => e.Unnamed18).HasColumnName("Unnamed:_18");
            entity.Property(e => e.Useable).HasColumnName("useable");
            entity.Property(e => e.Usesound).HasColumnName("usesound");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Wclass).HasColumnName("wclass");
            entity.Property(e => e._109SaveAdd).HasColumnName("1_09-SaveAdd");
            entity.Property(e => e._109SaveBits).HasColumnName("1_09-SaveBits");
            entity.Property(e => e._1or2handed).HasColumnName("1or2handed");
            entity.Property(e => e._2handed).HasColumnName("2handed");
            entity.Property(e => e._2handedwclass).HasColumnName("2handedwclass");
            entity.Property(e => e._2handmaxdam).HasColumnName("2handmaxdam");
            entity.Property(e => e._2handmindam).HasColumnName("2handmindam");
        });

        modelBuilder.Entity<Playerclass>(entity =>
        {
            entity.ToTable("playerclass");

            entity.Property(e => e.Code).HasColumnType("text(3)");
        });

        modelBuilder.Entity<Rune>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("runes");

            entity.Property(e => e.Complete).HasColumnName("complete");
            entity.Property(e => e.Eol).HasColumnName("eol");
            entity.Property(e => e.Etype1).HasColumnName("etype1");
            entity.Property(e => e.Etype2).HasColumnName("etype2");
            entity.Property(e => e.Etype3).HasColumnName("etype3");
            entity.Property(e => e.FirstLadderSeason).HasColumnName("firstLadderSeason");
            entity.Property(e => e.Itype1).HasColumnName("itype1");
            entity.Property(e => e.Itype2).HasColumnName("itype2");
            entity.Property(e => e.Itype3).HasColumnName("itype3");
            entity.Property(e => e.Itype4).HasColumnName("itype4");
            entity.Property(e => e.Itype5).HasColumnName("itype5");
            entity.Property(e => e.Itype6).HasColumnName("itype6");
            entity.Property(e => e.LastLadderSeason).HasColumnName("lastLadderSeason");
            entity.Property(e => e.T1code1).HasColumnName("T1Code1");
            entity.Property(e => e.T1code2).HasColumnName("T1Code2");
            entity.Property(e => e.T1code3).HasColumnName("T1Code3");
            entity.Property(e => e.T1code4).HasColumnName("T1Code4");
            entity.Property(e => e.T1code5).HasColumnName("T1Code5");
            entity.Property(e => e.T1code6).HasColumnName("T1Code6");
            entity.Property(e => e.T1code7).HasColumnName("T1Code7");
            entity.Property(e => e.T1max1).HasColumnName("T1Max1");
            entity.Property(e => e.T1max2).HasColumnName("T1Max2");
            entity.Property(e => e.T1max3).HasColumnName("T1Max3");
            entity.Property(e => e.T1max4).HasColumnName("T1Max4");
            entity.Property(e => e.T1max5).HasColumnName("T1Max5");
            entity.Property(e => e.T1max6).HasColumnName("T1Max6");
            entity.Property(e => e.T1max7).HasColumnName("T1Max7");
            entity.Property(e => e.T1min1).HasColumnName("T1Min1");
            entity.Property(e => e.T1min2).HasColumnName("T1Min2");
            entity.Property(e => e.T1min3).HasColumnName("T1Min3");
            entity.Property(e => e.T1min4).HasColumnName("T1Min4");
            entity.Property(e => e.T1min5).HasColumnName("T1Min5");
            entity.Property(e => e.T1min6).HasColumnName("T1Min6");
            entity.Property(e => e.T1min7).HasColumnName("T1Min7");
            entity.Property(e => e.T1param1).HasColumnName("T1Param1");
            entity.Property(e => e.T1param2).HasColumnName("T1Param2");
            entity.Property(e => e.T1param3).HasColumnName("T1Param3");
            entity.Property(e => e.T1param4).HasColumnName("T1Param4");
            entity.Property(e => e.T1param5).HasColumnName("T1Param5");
            entity.Property(e => e.T1param6).HasColumnName("T1Param6");
            entity.Property(e => e.T1param7).HasColumnName("T1Param7");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Aibonus).HasColumnName("aibonus");
            entity.Property(e => e.Aitype).HasColumnName("aitype");
            entity.Property(e => e.Alwayshit).HasColumnName("alwayshit");
            entity.Property(e => e.Anim).HasColumnName("anim");
            entity.Property(e => e.Attackrank).HasColumnName("attackrank");
            entity.Property(e => e.Aura).HasColumnName("aura");
            entity.Property(e => e.Auraevent1).HasColumnName("auraevent1");
            entity.Property(e => e.Auraevent2).HasColumnName("auraevent2");
            entity.Property(e => e.Auraevent3).HasColumnName("auraevent3");
            entity.Property(e => e.Auraeventfunc1).HasColumnName("auraeventfunc1");
            entity.Property(e => e.Auraeventfunc2).HasColumnName("auraeventfunc2");
            entity.Property(e => e.Auraeventfunc3).HasColumnName("auraeventfunc3");
            entity.Property(e => e.Aurafilter).HasColumnName("aurafilter");
            entity.Property(e => e.Auralencalc).HasColumnName("auralencalc");
            entity.Property(e => e.Aurarangecalc).HasColumnName("aurarangecalc");
            entity.Property(e => e.Aurastat1).HasColumnName("aurastat1");
            entity.Property(e => e.Aurastat2).HasColumnName("aurastat2");
            entity.Property(e => e.Aurastat3).HasColumnName("aurastat3");
            entity.Property(e => e.Aurastat4).HasColumnName("aurastat4");
            entity.Property(e => e.Aurastat5).HasColumnName("aurastat5");
            entity.Property(e => e.Aurastat6).HasColumnName("aurastat6");
            entity.Property(e => e.Aurastatcalc1).HasColumnName("aurastatcalc1");
            entity.Property(e => e.Aurastatcalc2).HasColumnName("aurastatcalc2");
            entity.Property(e => e.Aurastatcalc3).HasColumnName("aurastatcalc3");
            entity.Property(e => e.Aurastatcalc4).HasColumnName("aurastatcalc4");
            entity.Property(e => e.Aurastatcalc5).HasColumnName("aurastatcalc5");
            entity.Property(e => e.Aurastatcalc6).HasColumnName("aurastatcalc6");
            entity.Property(e => e.Aurastate).HasColumnName("aurastate");
            entity.Property(e => e.Auratargetstate).HasColumnName("auratargetstate");
            entity.Property(e => e.Calc1).HasColumnName("calc1");
            entity.Property(e => e.Calc1desc).HasColumnName("calc1desc");
            entity.Property(e => e.Calc2).HasColumnName("calc2");
            entity.Property(e => e.Calc2desc).HasColumnName("calc2desc");
            entity.Property(e => e.Calc3).HasColumnName("calc3");
            entity.Property(e => e.Calc3desc).HasColumnName("calc3desc");
            entity.Property(e => e.Calc4).HasColumnName("calc4");
            entity.Property(e => e.Calc4desc).HasColumnName("calc4desc");
            entity.Property(e => e.Calc5).HasColumnName("calc5");
            entity.Property(e => e.Calc5desc).HasColumnName("calc5desc");
            entity.Property(e => e.Calc6).HasColumnName("calc6");
            entity.Property(e => e.Calc6desc).HasColumnName("calc6desc");
            entity.Property(e => e.Castoverlay).HasColumnName("castoverlay");
            entity.Property(e => e.Charclass).HasColumnName("charclass");
            entity.Property(e => e.Cltcalc1).HasColumnName("cltcalc1");
            entity.Property(e => e.Cltcalc1desc).HasColumnName("cltcalc1desc");
            entity.Property(e => e.Cltcalc2).HasColumnName("cltcalc2");
            entity.Property(e => e.Cltcalc2desc).HasColumnName("cltcalc2desc");
            entity.Property(e => e.Cltcalc3).HasColumnName("cltcalc3");
            entity.Property(e => e.Cltcalc3desc).HasColumnName("cltcalc3desc");
            entity.Property(e => e.Cltdofunc).HasColumnName("cltdofunc");
            entity.Property(e => e.Cltmissile).HasColumnName("cltmissile");
            entity.Property(e => e.Cltmissilea).HasColumnName("cltmissilea");
            entity.Property(e => e.Cltmissileb).HasColumnName("cltmissileb");
            entity.Property(e => e.Cltmissilec).HasColumnName("cltmissilec");
            entity.Property(e => e.Cltmissiled).HasColumnName("cltmissiled");
            entity.Property(e => e.Cltoverlaya).HasColumnName("cltoverlaya");
            entity.Property(e => e.Cltoverlayb).HasColumnName("cltoverlayb");
            entity.Property(e => e.Cltprgfunc1).HasColumnName("cltprgfunc1");
            entity.Property(e => e.Cltprgfunc2).HasColumnName("cltprgfunc2");
            entity.Property(e => e.Cltprgfunc3).HasColumnName("cltprgfunc3");
            entity.Property(e => e.Cltstfunc).HasColumnName("cltstfunc");
            entity.Property(e => e.Cltstopfunc).HasColumnName("cltstopfunc");
            entity.Property(e => e.Costadd).HasColumnName("costadd");
            entity.Property(e => e.Costmult).HasColumnName("costmult");
            entity.Property(e => e.Decquant).HasColumnName("decquant");
            entity.Property(e => e.Dosound).HasColumnName("dosound");
            entity.Property(e => e.Dosounda).HasColumnName("dosounda");
            entity.Property(e => e.Dosoundb).HasColumnName("dosoundb");
            entity.Property(e => e.Durability).HasColumnName("durability");
            entity.Property(e => e.EdmgSymPerCalc).HasColumnName("EDmgSymPerCalc");
            entity.Property(e => e.Elen).HasColumnName("ELen");
            entity.Property(e => e.ElenSymPerCalc).HasColumnName("ELenSymPerCalc");
            entity.Property(e => e.ElevLen1).HasColumnName("ELevLen1");
            entity.Property(e => e.ElevLen2).HasColumnName("ELevLen2");
            entity.Property(e => e.ElevLen3).HasColumnName("ELevLen3");
            entity.Property(e => e.Emax).HasColumnName("EMax");
            entity.Property(e => e.EmaxLev1).HasColumnName("EMaxLev1");
            entity.Property(e => e.EmaxLev2).HasColumnName("EMaxLev2");
            entity.Property(e => e.EmaxLev3).HasColumnName("EMaxLev3");
            entity.Property(e => e.EmaxLev4).HasColumnName("EMaxLev4");
            entity.Property(e => e.EmaxLev5).HasColumnName("EMaxLev5");
            entity.Property(e => e.Emin).HasColumnName("EMin");
            entity.Property(e => e.EminLev1).HasColumnName("EMinLev1");
            entity.Property(e => e.EminLev2).HasColumnName("EMinLev2");
            entity.Property(e => e.EminLev3).HasColumnName("EMinLev3");
            entity.Property(e => e.EminLev4).HasColumnName("EMinLev4");
            entity.Property(e => e.EminLev5).HasColumnName("EMinLev5");
            entity.Property(e => e.Enhanceable).HasColumnName("enhanceable");
            entity.Property(e => e.Eol).HasColumnName("eol");
            entity.Property(e => e.Etype).HasColumnName("EType");
            entity.Property(e => e.Etypea1).HasColumnName("etypea1");
            entity.Property(e => e.Etypea2).HasColumnName("etypea2");
            entity.Property(e => e.Etypeb1).HasColumnName("etypeb1");
            entity.Property(e => e.Etypeb2).HasColumnName("etypeb2");
            entity.Property(e => e.Finishing).HasColumnName("finishing");
            entity.Property(e => e.Globaldelay).HasColumnName("globaldelay");
            entity.Property(e => e.Immediate).HasColumnName("immediate");
            entity.Property(e => e.Interrupt).HasColumnName("interrupt");
            entity.Property(e => e.Itypea1).HasColumnName("itypea1");
            entity.Property(e => e.Itypea2).HasColumnName("itypea2");
            entity.Property(e => e.Itypea3).HasColumnName("itypea3");
            entity.Property(e => e.Itypeb1).HasColumnName("itypeb1");
            entity.Property(e => e.Itypeb2).HasColumnName("itypeb2");
            entity.Property(e => e.Itypeb3).HasColumnName("itypeb3");
            entity.Property(e => e.Leftskill).HasColumnName("leftskill");
            entity.Property(e => e.Lob).HasColumnName("lob");
            entity.Property(e => e.Localdelay).HasColumnName("localdelay");
            entity.Property(e => e.Lvlmana).HasColumnName("lvlmana");
            entity.Property(e => e.Mana).HasColumnName("mana");
            entity.Property(e => e.Manashift).HasColumnName("manashift");
            entity.Property(e => e.Maxlvl).HasColumnName("maxlvl");
            entity.Property(e => e.Minmana).HasColumnName("minmana");
            entity.Property(e => e.Monanim).HasColumnName("monanim");
            entity.Property(e => e.Noammo).HasColumnName("noammo");
            entity.Property(e => e.Passive).HasColumnName("passive");
            entity.Property(e => e.Passivecalc1).HasColumnName("passivecalc1");
            entity.Property(e => e.Passivecalc10).HasColumnName("passivecalc10");
            entity.Property(e => e.Passivecalc11).HasColumnName("passivecalc11");
            entity.Property(e => e.Passivecalc12).HasColumnName("passivecalc12");
            entity.Property(e => e.Passivecalc13).HasColumnName("passivecalc13");
            entity.Property(e => e.Passivecalc14).HasColumnName("passivecalc14");
            entity.Property(e => e.Passivecalc2).HasColumnName("passivecalc2");
            entity.Property(e => e.Passivecalc3).HasColumnName("passivecalc3");
            entity.Property(e => e.Passivecalc4).HasColumnName("passivecalc4");
            entity.Property(e => e.Passivecalc5).HasColumnName("passivecalc5");
            entity.Property(e => e.Passivecalc6).HasColumnName("passivecalc6");
            entity.Property(e => e.Passivecalc7).HasColumnName("passivecalc7");
            entity.Property(e => e.Passivecalc8).HasColumnName("passivecalc8");
            entity.Property(e => e.Passivecalc9).HasColumnName("passivecalc9");
            entity.Property(e => e.Passiveitype).HasColumnName("passiveitype");
            entity.Property(e => e.Passivereqweaponcount).HasColumnName("passivereqweaponcount");
            entity.Property(e => e.Passivestat1).HasColumnName("passivestat1");
            entity.Property(e => e.Passivestat10).HasColumnName("passivestat10");
            entity.Property(e => e.Passivestat11).HasColumnName("passivestat11");
            entity.Property(e => e.Passivestat12).HasColumnName("passivestat12");
            entity.Property(e => e.Passivestat13).HasColumnName("passivestat13");
            entity.Property(e => e.Passivestat14).HasColumnName("passivestat14");
            entity.Property(e => e.Passivestat2).HasColumnName("passivestat2");
            entity.Property(e => e.Passivestat3).HasColumnName("passivestat3");
            entity.Property(e => e.Passivestat4).HasColumnName("passivestat4");
            entity.Property(e => e.Passivestat5).HasColumnName("passivestat5");
            entity.Property(e => e.Passivestat6).HasColumnName("passivestat6");
            entity.Property(e => e.Passivestat7).HasColumnName("passivestat7");
            entity.Property(e => e.Passivestat8).HasColumnName("passivestat8");
            entity.Property(e => e.Passivestat9).HasColumnName("passivestat9");
            entity.Property(e => e.Passivestate).HasColumnName("passivestate");
            entity.Property(e => e.Perdelay).HasColumnName("perdelay");
            entity.Property(e => e.Periodic).HasColumnName("periodic");
            entity.Property(e => e.Petmax).HasColumnName("petmax");
            entity.Property(e => e.Pettype).HasColumnName("pettype");
            entity.Property(e => e.Prgcalc1).HasColumnName("prgcalc1");
            entity.Property(e => e.Prgcalc2).HasColumnName("prgcalc2");
            entity.Property(e => e.Prgcalc3).HasColumnName("prgcalc3");
            entity.Property(e => e.Prgchargesconsumed).HasColumnName("prgchargesconsumed");
            entity.Property(e => e.Prgchargestocast).HasColumnName("prgchargestocast");
            entity.Property(e => e.Prgdam).HasColumnName("prgdam");
            entity.Property(e => e.Prgoverlay).HasColumnName("prgoverlay");
            entity.Property(e => e.Prgsound).HasColumnName("prgsound");
            entity.Property(e => e.Prgstack).HasColumnName("prgstack");
            entity.Property(e => e.Progressive).HasColumnName("progressive");
            entity.Property(e => e.Range).HasColumnName("range");
            entity.Property(e => e.Repeat).HasColumnName("repeat");
            entity.Property(e => e.Reqdex).HasColumnName("reqdex");
            entity.Property(e => e.Reqint).HasColumnName("reqint");
            entity.Property(e => e.Reqlevel).HasColumnName("reqlevel");
            entity.Property(e => e.Reqskill1).HasColumnName("reqskill1");
            entity.Property(e => e.Reqskill2).HasColumnName("reqskill2");
            entity.Property(e => e.Reqskill3).HasColumnName("reqskill3");
            entity.Property(e => e.Reqstr).HasColumnName("reqstr");
            entity.Property(e => e.Reqvit).HasColumnName("reqvit");
            entity.Property(e => e.Restrict).HasColumnName("restrict");
            entity.Property(e => e.Rightskill).HasColumnName("rightskill");
            entity.Property(e => e.Scroll).HasColumnName("scroll");
            entity.Property(e => e.SearchEnemyXy).HasColumnName("SearchEnemyXY");
            entity.Property(e => e.SearchOpenXy).HasColumnName("SearchOpenXY");
            entity.Property(e => e.Seqinput).HasColumnName("seqinput");
            entity.Property(e => e.Seqnum).HasColumnName("seqnum");
            entity.Property(e => e.Seqtrans).HasColumnName("seqtrans");
            entity.Property(e => e.Skill1).HasColumnName("skill");
            entity.Property(e => e.Skilldesc).HasColumnName("skilldesc");
            entity.Property(e => e.Skpoints).HasColumnName("skpoints");
            entity.Property(e => e.Srvdofunc).HasColumnName("srvdofunc");
            entity.Property(e => e.Srvmissile).HasColumnName("srvmissile");
            entity.Property(e => e.Srvmissilea).HasColumnName("srvmissilea");
            entity.Property(e => e.Srvmissileb).HasColumnName("srvmissileb");
            entity.Property(e => e.Srvmissilec).HasColumnName("srvmissilec");
            entity.Property(e => e.Srvoverlay).HasColumnName("srvoverlay");
            entity.Property(e => e.Srvprgfunc1).HasColumnName("srvprgfunc1");
            entity.Property(e => e.Srvprgfunc2).HasColumnName("srvprgfunc2");
            entity.Property(e => e.Srvprgfunc3).HasColumnName("srvprgfunc3");
            entity.Property(e => e.Srvstfunc).HasColumnName("srvstfunc");
            entity.Property(e => e.Srvstopfunc).HasColumnName("srvstopfunc");
            entity.Property(e => e.Startmana).HasColumnName("startmana");
            entity.Property(e => e.Stsound).HasColumnName("stsound");
            entity.Property(e => e.Stsoundclass).HasColumnName("stsoundclass");
            entity.Property(e => e.Stsounddelay).HasColumnName("stsounddelay");
            entity.Property(e => e.Stsuccessonly).HasColumnName("stsuccessonly");
            entity.Property(e => e.Summode).HasColumnName("summode");
            entity.Property(e => e.Summon).HasColumnName("summon");
            entity.Property(e => e.Sumoverlay).HasColumnName("sumoverlay");
            entity.Property(e => e.Sumsk1calc).HasColumnName("sumsk1calc");
            entity.Property(e => e.Sumsk2calc).HasColumnName("sumsk2calc");
            entity.Property(e => e.Sumsk3calc).HasColumnName("sumsk3calc");
            entity.Property(e => e.Sumsk4calc).HasColumnName("sumsk4calc");
            entity.Property(e => e.Sumsk5calc).HasColumnName("sumsk5calc");
            entity.Property(e => e.Sumskill1).HasColumnName("sumskill1");
            entity.Property(e => e.Sumskill2).HasColumnName("sumskill2");
            entity.Property(e => e.Sumskill3).HasColumnName("sumskill3");
            entity.Property(e => e.Sumskill4).HasColumnName("sumskill4");
            entity.Property(e => e.Sumskill5).HasColumnName("sumskill5");
            entity.Property(e => e.Sumumod).HasColumnName("sumumod");
            entity.Property(e => e.Tgtoverlay).HasColumnName("tgtoverlay");
            entity.Property(e => e.Tgtsound).HasColumnName("tgtsound");
            entity.Property(e => e.UseServerMissilesOnRemoteClients).HasColumnName("useServerMissilesOnRemoteClients");
            entity.Property(e => e.Usemanaondo).HasColumnName("usemanaondo");
            entity.Property(e => e.Warp).HasColumnName("warp");
            entity.Property(e => e.Weaponsnd).HasColumnName("weaponsnd");
            entity.Property(e => e.Weapsel).HasColumnName("weapsel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}