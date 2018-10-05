using System.IO;
using FlatBuffers;
using MyGame.Sample;
using UnityEngine;

public class FlatBuffersExample : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //create object, save it to bytes
        var buffer = Save();
        //Load bytes
        Load(buffer);
    }

    private void Load(byte[] bytes)
    {
        Debug.Log("Loading Monster from byte array");

        //We can use Unity's NativeArray<byte> implementation here.
        //Activated only when NATIVE_ARRAY_ALLOCATOR symbol is defined
        //If not, `new ByteBuffer(bytes);` should do.
        using (var buf = new ByteBuffer(new NativeArrayAllocator(bytes), 0))
        {
            // Get an accessor to the root object inside the buffer.
            var monster = Monster.GetRootAsMonster(buf);

            // For C#, unlike most other languages support by FlatBuffers, most values (except for
            // vectors and unions) are available as propreties instead of asccessor methods.
            var hp = monster.Hp;
            var mana = monster.Mana;
            var Name = monster.Name;

            Debug.LogFormat("Name: {0}, Hp: {1}, Mana:{2}", Name, hp, mana);

            var pos = monster.Pos.Value;
            var x = pos.X;
            var y = pos.Y;
            var z = pos.Z;

            Debug.LogFormat("Pos: {0}", new Vector3(x, y, z));

            int invLength = monster.InventoryLength;
            var thirdItem = monster.Inventory(2);

            Debug.LogFormat("Inventory Length: {0}, Third Item: {1}", invLength, thirdItem);

            int weaponsLength = monster.WeaponsLength;
            var secondWeaponName = monster.Weapons(1).Value.Name;
            var secondWeaponDamage = monster.Weapons(1).Value.Damage;

            Debug.LogFormat("Weapons count: {0}, Second Weapon Name: {1}, Second Weapon Damage: {2}", weaponsLength,
                secondWeaponName, secondWeaponDamage);

            var unionType = monster.EquippedType;
            if (unionType == Equipment.Weapon)
            {
                var weapon = monster.Equipped<Weapon>().Value;
                var weaponName = weapon.Name; // "Axe"
                var weaponDamage = weapon.Damage; // 5
                Debug.LogFormat("Equipped Weapon: {0}, Damage: {1}", weaponName, weaponDamage);
            }
        }
    }

    private byte[] Save()
    {
        Debug.Log("Creating and saving a Monster to byte array");
        // Create a `FlatBufferBuilder`, which will be used to create our
        // monsters' FlatBuffers.
        var builder = new FlatBufferBuilder(1024);

        var weaponOneName = builder.CreateString("Sword");
        var weaponOneDamage = 3;
        var weaponTwoName = builder.CreateString("Axe");
        var weaponTwoDamage = 5;
        // Use the `CreateWeapon()` helper function to create the weapons, since we set every field.
        var sword = Weapon.CreateWeapon(builder, weaponOneName, (short) weaponOneDamage);
        var axe = Weapon.CreateWeapon(builder, weaponTwoName, (short) weaponTwoDamage);

        // Serialize a name for our monster, called "Orc".
        var name = builder.CreateString("Orc");
        // Create a `vector` representing the inventory of the Orc. Each number
        // could correspond to an item that can be claimed after he is slain.
        // Note: Since we prepend the bytes, this loop iterates in reverse order.
        Monster.StartInventoryVector(builder, 10);
        for (int i = 9; i >= 0; i--)
        {
            builder.AddByte((byte) i);
        }

        var inv = builder.EndVector();

        var weaps = new Offset<Weapon>[2];
        weaps[0] = sword;
        weaps[1] = axe;
        // Pass the `weaps` array into the `CreateWeaponsVector()` method to create a FlatBuffer vector.
        var weapons = Monster.CreateWeaponsVector(builder, weaps);

        Monster.StartPathVector(builder, 2);
        Vec3.CreateVec3(builder, 1.0f, 2.0f, 3.0f);
        Vec3.CreateVec3(builder, 4.0f, 5.0f, 6.0f);
        var path = builder.EndVector();

        // Create our monster using `StartMonster()` and `EndMonster()`.
        Monster.StartMonster(builder);
        Monster.AddPos(builder, Vec3.CreateVec3(builder, 1.0f, 2.0f, 3.0f));
        Monster.AddHp(builder, 300);
        Monster.AddName(builder, name);
        Monster.AddInventory(builder, inv);
        Monster.AddColor(builder, MyGame.Sample.Color.Red);
        Monster.AddWeapons(builder, weapons);
        Monster.AddEquippedType(builder, Equipment.Weapon);
        Monster.AddEquipped(builder, axe.Value); // Axe
        Monster.AddPath(builder, path);
        var orc = Monster.EndMonster(builder);

        // Call `Finish()` to instruct the builder that this monster is complete.
        builder.Finish(orc.Value); // You could also call `Monster.FinishMonsterBuffer(builder, orc);`.

        return builder.SizedByteArray();
    }
}