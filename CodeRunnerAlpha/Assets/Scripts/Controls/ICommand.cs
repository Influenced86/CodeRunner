using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

interface ICommand
{
    void Execute(Player aPlayer);
}
