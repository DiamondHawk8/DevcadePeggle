using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Enum for making texturing pegs easier

public enum PegType
{
    // L represents lit (not sure what ima use that for yet)
    // G represents glowing, for pegs that have been hit and have not yet disappeared
    GREEN,
    GREENL,
    GREENG,
    BLUE,
    BLUEL,
    BLUEG,
    YELLOW,
    YELLOWL,
    YELLOWG,
    RED,
    REDL,
    REDG,
    ERROR
}