using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���͗p�̎��������
public class Dictionary : MonoBehaviour
{
    // �f�t�H���g�̓��͕��@
    public Dictionary<string, string> dic = new Dictionary<string, string>() {

        {"��", "a"},{"��", "i"},{"��", "u"},{"��", "e"},{"��", "o"},
        {"��", "ka"},{"��", "ki"},{"��", "ku"},{"��", "ke"},{"��", "ko"},
        {"��", "sa"},{"��", "si"},{"��", "su"},{"��", "se"},{"��", "so"},
        {"��", "ta"},{"��", "ti"},{"��", "tu"},{"��", "te"},{"��", "to"},
        {"��", "na"},{"��", "ni"},{"��", "nu"},{"��", "ne"},{"��", "no"},
        {"��", "ha"},{"��", "hi"},{"��", "hu"},{"��", "he"},{"��", "ho"},
        {"��", "ma"},{"��", "mi"},{"��", "mu"},{"��", "me"},{"��", "mo"},
        {"��", "ya"},{"��", "yu"},{"��", "yo"},
        {"��", "ra"},{"��", "ri"},{"��", "ru"},{"��", "re"},{"��", "ro"},
        {"��", "wa"},{"��", "wo"},{"��", "n"},
        {"��", "ga"},{"��", "gi"},{"��", "gu"},{"��", "ge"},{"��", "go"},
        {"��", "za"},{"��", "zi"},{"��", "zu"},{"��", "ze"},{"��", "zo"},
        {"��", "da"},{"��", "di"},{"��", "du"},{"��", "de"},{"��", "do"},
        {"��", "ba"},{"��", "bi"},{"��", "bu"},{"��", "be"},{"��", "bo"},
        {"��", "pa"},{"��", "pi"},{"��", "pu"},{"��", "pe"},{"��", "po"},
        {"��","xa" },{"��","xi" },{"��","xu" },{"��","xe" },{"��","xo" },
        {"��", "xtu"},
        {"��","xya" },{"��","xyu" },{"��","xyo"},
        {"����","kya"},{"����","kyi"},{"����","kyu"},{"����","kye"},{"����","kyo"},
        {"����","sya"},{"����","syi"},{"����","syu"},{"����","she"},{"����","syo"},
        {"����","tya"},{"����","tyi"},{"����","tyu"},{"����","tye"},{"����","tyo"},
        {"�ɂ�","nya"},{"�ɂ�","nyi"},{"�ɂ�","nyu"},{"�ɂ�","nye"},{"�ɂ�","nyo"},
        {"�Ђ�","hya"},{"�Ђ�","hyi"},{"�Ђ�","hyu"},{"�Ђ�","hye"},{"�Ђ�","hyo"},
        {"�݂�","mya"},{"�݂�","myi"},{"�݂�","myu"},{"�݂�","mye"},{"�݂�","myo"},
        {"���","rya"},{"�股","ryi"},{"���","ryu"},{"�肥","rye"},{"���","ryo"},
        {"����","gya"},{"����","gyi"},{"����","gyu"},{"����","gye"},{"����","gyo"},
        {"����","zya"},{"����","zhi"},{"����","zyu"},{"����","zye"},{"����","zyo"},
        {"����","dya"},{"����","dyi"},{"����","dyu"},{"����","dye"},{"����","dyo"},
        {"�т�","bya"},{"�т�","byi"},{"�т�","byu"},{"�т�","bye"},{"�т�","byo"},
        {"�Ă�","tha"},{"�Ă�","thi"},{"�Ă�","thu"},{"�Ă�","the"},{"�Ă�","tho"},
        {"����","wha"},{"����","whi"},{"����","whe"},{"����","who"},
        {"�ł�","dha"},{"�ł�","dhi"},{"�ł�","dhu"},{"�ł�","dhe"},{"�ł�","dho"},
        {"����","qa"},{"����","qi"},{"����","qe"},{"����","qo"},
        {"�ӂ�","fa"},{"�ӂ�","fi"},{"�ӂ�","fe"},{"�ӂ�","fo"},
        {"����","va"},{"����","vi"},{"��","vu"},{"����","ve"},{"����","vo"},
        {"�҂�","pya"},{"�҂�","pyi"},{"�҂�","pyu"},{"�҂�","pye"},{"�҂�","pyo"},
        {"�A","," },{"�B","."},{"�u","["},{"�v","]"},
    };

    // �f�t�H���g�ł͂Ȃ����͕��@
    public Dictionary<string, List<string>> dicEx = new Dictionary<string, List<string>>()
    {
        {"��", new List<string>{"a"}},{"��", new List<string>{"i"}},{"��", new List<string>{"u"}},{"��", new List<string>{"e"}},{"��", new List<string>{"o"}},
        {"��", new List<string>{"ca"}},{"��", new List<string>{"ki"}},{"��", new List<string>{"cu","qu"}},{"��", new List<string>{"ke"}},{"��", new List<string>{"co"}},
        {"��", new List<string>{"sa"}},{"��", new List<string>{"ci","shi"}},{"��", new List<string>{"su"}},{"��", new List<string>{"ce"}},{"��", new List<string>{"so"}},
        {"��", new List<string>{"a"}},{"��", new List<string>{"chi","cyi"}},{"��", new List<string>{"tsu"}},{"��", new List<string>{"te"}},{"��", new List<string>{"to"}},
        {"����", new List<string>{"cha","cya"}},{"����", new List<string>{"chu","cyu"}},{"����", new List<string>{"che","cye"}},{"����", new List<string>{"cho","cyo"}},
        {"��", new List<string>{"na"}},{"��", new List<string>{"ni"}},{"��", new List<string>{"nu"}},{"��", new List<string>{"ne"}},{"��", new List<string>{"no"}},
        {"��", new List<string>{"ha"}},{"��", new List<string>{"hi"}},{"��", new List<string>{"fu"}},{"��", new List<string>{"he"}},{"��", new List<string>{"ho"}},
        {"��", new List<string>{"ma"}},{"��", new List<string>{"mi"}},{"��", new List<string>{"mu"}},{"��", new List<string>{"me"}},{"��", new List<string>{"mo"}},
        {"��", new List<string>{"ya"}},{"��", new List<string>{"yu"}},{"��", new List<string>{"yo"}},
        {"��", new List<string>{"ra"}},{"��", new List<string>{"ri"}},{"��", new List<string>{"ru"}},{"��", new List<string>{"re"}},{"��", new List<string>{"ro"}},
        {"��", new List<string>{"wa"}},{"��", new List<string>{"wo"}},{"��", new List<string>{"nn"}},
        {"��", new List<string>{"ga"}},{"��", new List<string>{"gi"}},{"��", new List<string>{"gu"}},{"��", new List<string>{"ge"}},{"��", new List<string>{"go"}},
        {"��", new List<string>{"za"}},{"��", new List<string>{"ji"}},{"��", new List<string>{"zu"}},{"��", new List<string>{"ze"}},{"��", new List<string>{"zo"}},
        {"����", new List<string>{"ja"}},{"����", new List<string>{"ju"}},{"����", new List<string>{"je"}},{"����", new List<string>{"jo"}},
        {"��", new List<string>{"da"}},{"��", new List<string>{"di"}},{"��", new List<string>{"du"}},{"��", new List<string>{"de"}},{"��", new List<string>{"do"}},
        {"��", new List<string>{"ba"}},{"��", new List<string>{"bi"}},{"��", new List<string>{"bu"}},{"��", new List<string>{"be"}},{"��", new List<string>{"bo"}},
        {"��", new List<string>{"pa"}},{"��", new List<string>{"pi"}},{"��", new List<string>{"pu"}},{"��", new List<string>{"pe"}},{"��", new List<string>{"po"}},
        {"��", new List<string>{"la","xa"}},{"��", new List<string>{"li","xi"}},{"��", new List<string>{"lu","xu"}},{"��", new List<string>{"le","xe"}},{"��", new List<string>{"lo","xo"}},
        {"��", new List<string>{"ltu","xtu"}},
        {"��", new List<string>{"lya","xya"}},{"��", new List<string>{"lyu","xyu"}},{"��", new List<string>{"lyo","xyo"}},

        {"����", new List<string>{"qyi"}},{"����", new List<string>{"qye"}},
        {"����", new List<string>{"sha"}},{"����", new List<string>{"shu"}},{"����", new List<string>{"she"}},{"����", new List<string>{"sho"}},
    };
}