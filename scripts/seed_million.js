// scripts/seed_million.js
// Ejecuta con: mongosh .\scripts\seed_million.js

const dbName = "MillionDB";
const col = "properties";

use(dbName);

// Collation para comparaciones insensibles a mayúsculas/minúsculas (útil para ordenaciones)
if (!db.getCollectionNames().includes(col)) {
  db.createCollection(col, { collation: { locale: "es", strength: 2 } });
  print(`Colección '${col}' creada`);
}

// Índices recomendados según tus filtros (name, address, price)
db[col].createIndex({ name: 1 },    { name: "idx_name" });
db[col].createIndex({ address: 1 }, { name: "idx_address" });
db[col].createIndex({ price: 1 },   { name: "idx_price" });

// (Opcional) Búsqueda de texto en name+address
db[col].createIndex({ name: "text", address: "text" }, { name: "idx_text_name_address", default_language: "spanish" });

// Datos de ejemplo (precio como Decimal128)
if (db[col].countDocuments() === 0) {
  db[col].insertMany([
    {
      name: "Casa Centro",
      address: "Av. Principal 123",
      price: NumberDecimal("120000"),
      idOwner: "OW-0001",
      image: "https://picsum.photos/seed/casa1/600/400",
      createdAt: new Date(),
      updatedAt: new Date()
    },
    {
      name: "Departamento Norte",
      address: "Calle 45 #10-22",
      price: NumberDecimal("85000"),
      idOwner: "OW-0002",
      image: "https://picsum.photos/seed/dep1/600/400",
      createdAt: new Date(),
      updatedAt: new Date()
    }
  ]);
  print("Datos de ejemplo insertados");
} else {
  print("La colección ya tiene datos, no se insertó seed");
}

print("Índices actuales:");
printjson(db[col].getIndexes());
