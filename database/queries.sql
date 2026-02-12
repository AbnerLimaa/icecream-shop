--create tables
create table tb_menus
(
    menu_id serial primary key,
    menu_name varchar(100) not null
);

create table tb_menu_items
(
    menu_item_id serial primary key,
    menu_id integer not null,
    flavor varchar(100) not null,
    price float,

    constraint fk_menu
    foreign key(menu_id)
    references tb_menus(menu_id)
    on delete cascade
);

create table tb_clients
(
    client_id serial primary key,
    client_name varchar(100) not null    
);

create table tb_orders
(
    order_id integer not null,
    menu_item_id integer not null,
    client_id integer not null,
    quantity integer not null,
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    primary key (order_id, menu_item_id),

    constraint fk_client
    foreign key(client_id)
    references tb_clients(client_id)
    on delete cascade,

    constraint fk_menu_item
    foreign key(menu_item_id)
    references tb_menu_items(menu_item_id)
    on delete cascade
);

--populate menu table
insert into tb_menus (menu_id, menu_name)
values (1, 'summer menu'),
       (2, 'fall menu'),
       (3, 'winter menu'),
       (4, 'spring menu');

insert into tb_menu_items (menu_item_id, menu_id, flavor, price)
values (1, 1, 'chocolate', 5.0),
       (2, 1, 'vanilla', 6.5),
       (3, 1, 'strawberry', 4.5),
       (4, 1, 'lemon', 5.25),

       (5, 2, 'chocolate', 4.0),
       (6, 2, 'vanilla', 6.0),
       (7, 2, 'strawberry', 4.2),
       (8, 2, 'lemon', 5.30),

       (9, 3, 'chocolate', 4.5),
       (10, 3, 'vanilla', 6.25),
       (11, 3, 'strawberry', 4.7),
       (12, 3, 'lemon', 5.15),
       
       (13, 4, 'chocolate', 4.75),
       (14, 4, 'vanilla', 6.3),
       (15, 4, 'strawberry', 4.4),
       (16, 4, 'lemon', 5.8);

insert into tb_clients (client_id, client_name)
values (1, 'Abner'),
       (2, 'Carol');

insert into tb_orders (order_id, client_id, order_date, menu_item_id, quantity)
values (1, 1, '2026-02-01', 6, 3),
       (1, 1, '2026-02-01', 7, 1),
       (1, 1, '2026-02-01', 12, 2),
       (2, 1, '2026-02-05', 2, 5),
       (2, 1, '2026-02-05', 15, 1),
       (3, 2, '2026-02-03', 10, 2),
       (3, 2, '2026-02-03', 13, 1),
       (3, 2, '2026-02-03', 5, 2),
       (3, 2, '2026-02-03', 3, 1)
       

select * from tb_menus;
select * from tb_menu_items;
select * from tb_clients;

select menu.menu_name, items.flavor, items.price
from tb_menu_items as items
inner join tb_menus as menu on items.menu_id = menu.menu_id
where items.menu_id=1;

select orders.order_id, clients.client_name, items.flavor, orders.quantity, orders.order_date
from tb_orders as orders
inner join tb_menu_items items on orders.menu_item_id = items.menu_item_id
inner join tb_clients clients on orders.client_id = clients.client_id;