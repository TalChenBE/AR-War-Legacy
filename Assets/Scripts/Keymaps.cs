using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keymaps 
{ 
    public static class Keymap_names 
    {
        public static string language;

        static Dictionary<string, string>  key_en = new Dictionary<string, string>();
        static Dictionary<string, string> key_he = new Dictionary<string, string>();

        public static void keymap_names_init()
        {
            language = "English";
            loadEnglish();
        }


        public static void keymap_names_init(string selected_language)
        {
            language = selected_language;

            if (selected_language.Equals("Hebrew"))
                loadHebrew();
            else loadEnglish();
        }

        // -- utils: load the english or hebrew files -- //
        private static void loadEnglish()
        {
            // places names
            key_en["ramthnia_name"] = "Ramthnia";

            // forces 
            key_en["danon_force"] = "Danon Force";
            key_en["harel_force"] = "Harel Force";

            // speakers names
            key_en["harel_name"] = "Harel";
            key_en["danon_name"] = "Haim Danon, Battalion Commander";

            // button
            key_en["skip_btn"] = "Skip";
            key_en["overview_btn"] = "Overview";
            key_en["AR_btn"] = "AR";

            // -- dates:
            key_en["monday_afternoon"] = "08.10.1973 \n Monday afternoon";
            key_en["tuesday_dawn"] = "09.10.1973 \n Tuesday at dawn";
            key_en["tuesday_afternoon"] = "09.10.1973 \n Tuesday Afternoon";

        }

        private static void loadHebrew()
        {
            // places names
            key_he["ramthnia_name"] = "רמתנייה";

            // forces 
            key_he["danon_force"] = "כוח דנון";
            key_he["harel_force"] = "כוח הראל";

            // speakers names
            key_he["harel_name"] = "הראל";
            key_he["danon_name"] = "חיים דנון, מג\"ד";
            
            // button
            key_he["skip_btn"] = "דלג";
            key_he["overview_btn"] = "מבט על";
            key_he["AR_btn"] = "RA";

            // -- dates:
            key_he["monday_afternoon"] = "יום שני אחר הצהריים \n י\"ב בתשרי תשל\"ד \n 3791.01.80";
            key_he["tuesday_dawn"] = "יום שלישי עם שחר \n י\"ג בתשרי תשל\"ד \n 3791.01.90";
            key_he["tuesday_afternoon"] = "יום שלישי אחה\"צ \n י\"ג בתשרי תשל\"ד \n 3791.01.90";

        }

        // -- Getters & Setters -- //
        public static void setLanguage(string selected_language)
        {
            language = selected_language;
        }


        public static string getLanguage()
        {
            return language;
        }

        public static string getValue(string key)
        {
            if (language == "Hebrew")
                return key_he[key];

            return key_en[key];
        }
    }
}
