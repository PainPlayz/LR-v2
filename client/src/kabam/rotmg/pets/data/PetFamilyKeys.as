package kabam.rotmg.pets.data {
   public class PetFamilyKeys {

      private static const keys:Object = {
         "Humanoid":"Pets.humanoid",
         "Feline":"Pets.feline",
         "Canine":"Pets.canine",
         "Avian":"Pets.avian",
         "Exotic":"Pets.exotic",
         "Farm":"pets.Farm",
         "Woodland":"Pets.woodland",
         "Reptile":"Pets.reptile",
         "Insect":"pets.Insect",
         "Penguin":"pets.Penguin",
         "Aquatic":"Pets.aquatic",
         "Spooky":"Pets.spooky",
         "Automaton":"Pets.automaton"
      };

      public function PetFamilyKeys() {
         super();
      }

      public static function getTranslationKey(param1:String) : String {
         var _local2:String = keys[param1];
         _local2 = _local2 || (param1 == "? ? ? ?"?"Pets.miscellaneous":"");
         return _local2;
      }
   }
}
