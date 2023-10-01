﻿using D2SLib2.BinaryHandler;
using D2SLib2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Player
{
    public class SkillsClass
    {
        public HashSet<SkillClass> SkillList = new HashSet<SkillClass>();

        public SkillsClass() { }

        public static SkillsClass Read(BitwiseBinaryReader mainReader, Player.PlayerClass playerClass)
        {
            FindSkillOffsetInBytes(mainReader);
            if (mainReader.ReadBits(SkillsOffsets.OFFSET_START_SEARCH.BitLength).ToStr() != SkillsOffsets.OFFSET_START_SEARCH.Signature)
                throw new OffsetException("Unable to find Skill offset with search, corrupt save?");

            SkillsClass skills = new SkillsClass();
            skills.SkillList = new HashSet<SkillClass>();

            Model.Playerclass? _class = D2S.instance?.dbContext?.Playerclasses?.SingleOrDefault(x => x.Id - 1 == (int) playerClass);
            if(_class == null)
                throw new Exception("A database error occurred when searching for your class by ID");

            int skillOffsetStart = (int)(D2S.instance?.dbContext?.Skills?.FirstOrDefault(x => x.Charclass == _class!.Code)?.Id ?? -1);
            if (skillOffsetStart <= -1)
                throw new Exception("Unable to find Skill offset from database, corrupt save?");

            for(int i=0;i<30;i++)
            {
                skills.SkillList.Add(new SkillClass() { Id = skillOffsetStart + i, Value = mainReader.ReadBits(SkillsOffsets.OFFSET_SKILL.BitLength).ToByte() });
            }

            return skills;
        }

        public static void FindSkillOffsetInBytes(BitwiseBinaryReader mainReader)
        {
            mainReader.SetBytePosition(SkillsOffsets.OFFSET_START_SEARCH.Offset);
            while (mainReader.PeekBits(16).ToStr() != SkillsOffsets.OFFSET_START_SEARCH.Signature) mainReader.ReadByte();
        }
    }

    [DebuggerDisplay("{Id} : {Value}")]
    public class SkillClass
    {
        public int Id { get; set; } = -1;
        public int Value { get; set; } = -1;
    }
}