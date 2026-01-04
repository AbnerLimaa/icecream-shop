--create tables
create table tb_menu
(
    menu_id serial primary key,
    flavor varchar(100) not null,
    price float
);

create table tb_orders
(
    order_id integer not null,
    menu_id integer not null,
    client_name varchar(100) not null,
    quantity integer not null,
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    primary key (order_id, menu_id),

    constraint fk_menu
      foreign key(menu_id)
      references tb_menu(menu_id)
      on delete cascade
);

--populate menu table
insert into tb_menu (menu_id, flavor, price)
values (1, 'chocolate', 5.0),
       (2, 'vanilla', 6.5),
       (3, 'strawberry', 4.5),
       (4, 'lemon', 5.25);

select * from tb_menu;