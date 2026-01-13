import psycopg2
from faker import Faker
import random
import re

# Configure your PostgreSQL connection
conn = psycopg2.connect(
    host="localhost",
    database="Restaurants",
    user="postgres",
    password="pgpass",
    port=5432
)
cursor = conn.cursor()

# 🔁 Restart the IDENTITY sequence to begin at 99999
cursor.execute('ALTER SEQUENCE "Restaurants_Id_seq" RESTART WITH 99999;')
conn.commit()

faker = Faker()

# ✅ Valid categories (from FluentValidation)
categories = [
    "Fast Food", "Italian", "Chinese", "Mexican", "American",
    "Japanese", "Indian", "Thai", "Greek", "French",
    "Vegetarian", "Vegan", "Other"
]

# Fixed OwnerId
owner_id = "76e59613-9887-4172-8c79-64615b1f2aaf"

insert_query = """
    INSERT INTO public."Restaurants"
    ("Name", "Description", "Category", "HasDelivery", "ContactEmail", "ContactNumber",
     "Address_City", "Address_Street", "Address_PostalCode", "OwnerId")
    VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
"""

batch_size = 100
total_rows = 5000

def generate_valid_phone():
    # Generate a phone like: +123-456789012
    country_code = f"+{random.randint(100, 999)}"
    number = f"{random.randint(100000, 999999999999)}"
    return f"{country_code}-{number}"

def generate_valid_postal_code():
    return f"{random.randint(10, 99)}-{random.randint(100, 999)}"

for i in range(total_rows):
    # Name: 3 to 100 characters
    name = faker.company()
    if len(name) < 3:
        name += " Inc"
    name = name[:100]

    # Description: non-empty
    description = faker.text(max_nb_chars=150)

    # Valid category
    category = random.choice(categories)

    # Has delivery
    has_delivery = random.choice([True, False])

    # Valid email
    email = faker.email()

    # Valid phone number
    phone = generate_valid_phone()

    # Valid postal code
    postal_code = generate_valid_postal_code()

    # Address
    city = faker.city()
    street = faker.street_address()

    values = (name, description, category, has_delivery, email, phone, city, street, postal_code, owner_id)
    cursor.execute(insert_query, values)

    if (i + 1) % batch_size == 0:
        conn.commit()
        print(f"Inserted {i + 1} records...")

# Final commit
conn.commit()
cursor.close()
conn.close()
print("✅ Successfully inserted 5000 records starting from ID 99999 with fixed OwnerId and all validations satisfied.")
