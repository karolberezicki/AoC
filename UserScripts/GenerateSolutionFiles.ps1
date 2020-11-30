param(
    [int]$Year = (Get-Date).Year
)

$template = @"
using System;

namespace AoC._<YEAR>.Day<DAY>
{
    public class Day<DAY> : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines();

            var part1 = "";
            var part2 = "";

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}

"@

$newDirectory = Join-Path $PSScriptRoot ".." "AoC" "$Year" 

if (!(Test-Path $newDirectory)) {
    New-Item $newDirectory -ItemType Directory | Out-Null
}

for ($i = 1; $i -le 25; $i++) {
    $newFile = Join-Path $newDirectory "Day$("{0:00}" -f $i)"  "Day$("{0:00}" -f $i).cs"  
    if (!(Test-Path $newFile)) {
        New-Item $newFile -ItemType File -Value ($template -replace "<YEAR>", $Year -replace "<DAY>", "$("{0:00}" -f $i)") -Force | Out-Null
    }
}

Write-Host "Files Generated"