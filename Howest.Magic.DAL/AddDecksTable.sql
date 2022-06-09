use mtg_v1

create table decks (
id int primary key identity,
cards nvarchar(max),
password nvarchar(max)
)