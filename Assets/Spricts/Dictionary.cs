using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 入力用の辞書を作る
public class Dictionary : MonoBehaviour
{
    // デフォルトの入力方法
    public Dictionary<string, string> dic = new Dictionary<string, string>() {

        {"あ", "a"},{"い", "i"},{"う", "u"},{"え", "e"},{"お", "o"},
        {"か", "ka"},{"き", "ki"},{"く", "ku"},{"け", "ke"},{"こ", "ko"},
        {"さ", "sa"},{"し", "si"},{"す", "su"},{"せ", "se"},{"そ", "so"},
        {"た", "ta"},{"ち", "ti"},{"つ", "tu"},{"て", "te"},{"と", "to"},
        {"な", "na"},{"に", "ni"},{"ぬ", "nu"},{"ね", "ne"},{"の", "no"},
        {"は", "ha"},{"ひ", "hi"},{"ふ", "hu"},{"へ", "he"},{"ほ", "ho"},
        {"ま", "ma"},{"み", "mi"},{"む", "mu"},{"め", "me"},{"も", "mo"},
        {"や", "ya"},{"ゆ", "yu"},{"よ", "yo"},
        {"ら", "ra"},{"り", "ri"},{"る", "ru"},{"れ", "re"},{"ろ", "ro"},
        {"わ", "wa"},{"を", "wo"},{"ん", "n"},
        {"が", "ga"},{"ぎ", "gi"},{"ぐ", "gu"},{"げ", "ge"},{"ご", "go"},
        {"ざ", "za"},{"じ", "zi"},{"ず", "zu"},{"ぜ", "ze"},{"ぞ", "zo"},
        {"だ", "da"},{"ぢ", "di"},{"づ", "du"},{"で", "de"},{"ど", "do"},
        {"ば", "ba"},{"び", "bi"},{"ぶ", "bu"},{"べ", "be"},{"ぼ", "bo"},
        {"ぱ", "pa"},{"ぴ", "pi"},{"ぷ", "pu"},{"ぺ", "pe"},{"ぽ", "po"},
        {"ぁ","xa" },{"ぃ","xi" },{"ぅ","xu" },{"ぇ","xe" },{"ぉ","xo" },
        {"っ", "xtu"},
        {"ゃ","xya" },{"ゅ","xyu" },{"ょ","xyo"},
        {"きゃ","kya"},{"きぃ","kyi"},{"きゅ","kyu"},{"きぇ","kye"},{"きょ","kyo"},
        {"しゃ","sya"},{"しぃ","syi"},{"しゅ","syu"},{"しぇ","she"},{"しょ","syo"},
        {"ちゃ","tya"},{"ちぃ","tyi"},{"ちゅ","tyu"},{"ちぇ","tye"},{"ちょ","tyo"},
        {"にゃ","nya"},{"にぃ","nyi"},{"にゅ","nyu"},{"にぇ","nye"},{"にょ","nyo"},
        {"ひゃ","hya"},{"ひぃ","hyi"},{"ひゅ","hyu"},{"ひぇ","hye"},{"ひょ","hyo"},
        {"みゃ","mya"},{"みぃ","myi"},{"みゅ","myu"},{"みぇ","mye"},{"みょ","myo"},
        {"りゃ","rya"},{"りぃ","ryi"},{"りゅ","ryu"},{"りぇ","rye"},{"りょ","ryo"},
        {"ぎゃ","gya"},{"ぎぃ","gyi"},{"ぎゅ","gyu"},{"ぎぇ","gye"},{"ぎょ","gyo"},
        {"じゃ","zya"},{"じぃ","zhi"},{"じゅ","zyu"},{"じぇ","zye"},{"じょ","zyo"},
        {"ぢゃ","dya"},{"ぢぃ","dyi"},{"ぢゅ","dyu"},{"ぢぇ","dye"},{"ぢょ","dyo"},
        {"びゃ","bya"},{"びぃ","byi"},{"びゅ","byu"},{"びぇ","bye"},{"びょ","byo"},
        {"てゃ","tha"},{"てぃ","thi"},{"てゅ","thu"},{"てぇ","the"},{"てょ","tho"},
        {"うぁ","wha"},{"うぃ","whi"},{"うぇ","whe"},{"うぉ","who"},
        {"でゃ","dha"},{"でぃ","dhi"},{"でゅ","dhu"},{"でぇ","dhe"},{"でょ","dho"},
        {"くぁ","qa"},{"くぃ","qi"},{"くぇ","qe"},{"くぉ","qo"},
        {"ふぁ","fa"},{"ふぃ","fi"},{"ふぇ","fe"},{"ふぉ","fo"},
        {"ヴぁ","va"},{"ヴぃ","vi"},{"ヴ","vu"},{"ヴぇ","ve"},{"ヴぉ","vo"},
        {"ぴゃ","pya"},{"ぴぃ","pyi"},{"ぴゅ","pyu"},{"ぴぇ","pye"},{"ぴょ","pyo"},
        {"、","," },{"。","."},{"「","["},{"」","]"},
    };

    // デフォルトではない入力方法
    public Dictionary<string, List<string>> dicEx = new Dictionary<string, List<string>>()
    {
        {"あ", new List<string>{"a"}},{"い", new List<string>{"i"}},{"う", new List<string>{"u"}},{"え", new List<string>{"e"}},{"お", new List<string>{"o"}},
        {"か", new List<string>{"ca"}},{"き", new List<string>{"ki"}},{"く", new List<string>{"cu","qu"}},{"け", new List<string>{"ke"}},{"こ", new List<string>{"co"}},
        {"さ", new List<string>{"sa"}},{"し", new List<string>{"ci","shi"}},{"す", new List<string>{"su"}},{"せ", new List<string>{"ce"}},{"そ", new List<string>{"so"}},
        {"た", new List<string>{"a"}},{"ち", new List<string>{"chi","cyi"}},{"つ", new List<string>{"tsu"}},{"て", new List<string>{"te"}},{"と", new List<string>{"to"}},
        {"ちゃ", new List<string>{"cha","cya"}},{"ちゅ", new List<string>{"chu","cyu"}},{"ちぇ", new List<string>{"che","cye"}},{"ちょ", new List<string>{"cho","cyo"}},
        {"な", new List<string>{"na"}},{"に", new List<string>{"ni"}},{"ぬ", new List<string>{"nu"}},{"ね", new List<string>{"ne"}},{"の", new List<string>{"no"}},
        {"は", new List<string>{"ha"}},{"ひ", new List<string>{"hi"}},{"ふ", new List<string>{"fu"}},{"へ", new List<string>{"he"}},{"ほ", new List<string>{"ho"}},
        {"ま", new List<string>{"ma"}},{"み", new List<string>{"mi"}},{"む", new List<string>{"mu"}},{"め", new List<string>{"me"}},{"も", new List<string>{"mo"}},
        {"や", new List<string>{"ya"}},{"ゆ", new List<string>{"yu"}},{"よ", new List<string>{"yo"}},
        {"ら", new List<string>{"ra"}},{"り", new List<string>{"ri"}},{"る", new List<string>{"ru"}},{"れ", new List<string>{"re"}},{"ろ", new List<string>{"ro"}},
        {"わ", new List<string>{"wa"}},{"を", new List<string>{"wo"}},{"ん", new List<string>{"nn"}},
        {"が", new List<string>{"ga"}},{"ぎ", new List<string>{"gi"}},{"ぐ", new List<string>{"gu"}},{"げ", new List<string>{"ge"}},{"ご", new List<string>{"go"}},
        {"ざ", new List<string>{"za"}},{"じ", new List<string>{"ji"}},{"ず", new List<string>{"zu"}},{"ぜ", new List<string>{"ze"}},{"ぞ", new List<string>{"zo"}},
        {"じゃ", new List<string>{"ja"}},{"じゅ", new List<string>{"ju"}},{"じぇ", new List<string>{"je"}},{"じょ", new List<string>{"jo"}},
        {"だ", new List<string>{"da"}},{"ぢ", new List<string>{"di"}},{"づ", new List<string>{"du"}},{"で", new List<string>{"de"}},{"ど", new List<string>{"do"}},
        {"ば", new List<string>{"ba"}},{"び", new List<string>{"bi"}},{"ぶ", new List<string>{"bu"}},{"べ", new List<string>{"be"}},{"ぼ", new List<string>{"bo"}},
        {"ぱ", new List<string>{"pa"}},{"ぴ", new List<string>{"pi"}},{"ぷ", new List<string>{"pu"}},{"ぺ", new List<string>{"pe"}},{"ぽ", new List<string>{"po"}},
        {"ぁ", new List<string>{"la","xa"}},{"ぃ", new List<string>{"li","xi"}},{"ぅ", new List<string>{"lu","xu"}},{"ぇ", new List<string>{"le","xe"}},{"ぉ", new List<string>{"lo","xo"}},
        {"っ", new List<string>{"ltu","xtu"}},
        {"ゃ", new List<string>{"lya","xya"}},{"ゅ", new List<string>{"lyu","xyu"}},{"ょ", new List<string>{"lyo","xyo"}},

        {"くぃ", new List<string>{"qyi"}},{"くぇ", new List<string>{"qye"}},
        {"しゃ", new List<string>{"sha"}},{"しゅ", new List<string>{"shu"}},{"しぇ", new List<string>{"she"}},{"しょ", new List<string>{"sho"}},
    };
}