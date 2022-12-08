--
-- PostgreSQL database dump
--

-- Dumped from database version 14.5
-- Dumped by pg_dump version 14.5

-- Started on 2022-12-06 22:28:33

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 210 (class 1259 OID 35309)
-- Name: accounts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.accounts (
    id integer NOT NULL,
    username character varying,
    password character varying,
    email character varying,
    alamat character varying,
    name character varying,
    nomor_hp character varying,
    role character varying,
    created_at timestamp without time zone,
    updated_at timestamp without time zone
);


ALTER TABLE public.accounts OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 35308)
-- Name: accounts_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.accounts_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.accounts_id_seq OWNER TO postgres;

--
-- TOC entry 3353 (class 0 OID 0)
-- Dependencies: 209
-- Name: accounts_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.accounts_id_seq OWNED BY public.accounts.id;


--
-- TOC entry 214 (class 1259 OID 35336)
-- Name: orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orders (
    id integer NOT NULL,
    username character varying,
    id_paket integer,
    nama_booking character varying,
    tanggal_booking date,
    dp integer,
    status character varying,
    created_at timestamp without time zone,
    updated_at timestamp without time zone
);


ALTER TABLE public.orders OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 35335)
-- Name: orders_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.orders_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.orders_id_seq OWNER TO postgres;

--
-- TOC entry 3354 (class 0 OID 0)
-- Dependencies: 213
-- Name: orders_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.orders_id_seq OWNED BY public.orders.id;


--
-- TOC entry 212 (class 1259 OID 35318)
-- Name: packets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.packets (
    id integer NOT NULL,
    jenis_paket character varying,
    jenis_foto character varying,
    ukuran_foto character varying,
    keterangan text,
    biaya integer
);


ALTER TABLE public.packets OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 35317)
-- Name: packets_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.packets_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.packets_id_seq OWNER TO postgres;

--
-- TOC entry 3355 (class 0 OID 0)
-- Dependencies: 211
-- Name: packets_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.packets_id_seq OWNED BY public.packets.id;


--
-- TOC entry 218 (class 1259 OID 35373)
-- Name: photographers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.photographers (
    id integer NOT NULL,
    nama character varying,
    status character varying,
    jadwal date
);


ALTER TABLE public.photographers OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 35372)
-- Name: photographers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.photographers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.photographers_id_seq OWNER TO postgres;

--
-- TOC entry 3356 (class 0 OID 0)
-- Dependencies: 217
-- Name: photographers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.photographers_id_seq OWNED BY public.photographers.id;


--
-- TOC entry 216 (class 1259 OID 35345)
-- Name: transactions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transactions (
    id integer NOT NULL,
    orders_id integer,
    total integer,
    metode_pembayaran character varying,
    status character varying,
    created_at timestamp without time zone
);


ALTER TABLE public.transactions OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 35344)
-- Name: transactions_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.transactions_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.transactions_id_seq OWNER TO postgres;

--
-- TOC entry 3357 (class 0 OID 0)
-- Dependencies: 215
-- Name: transactions_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.transactions_id_seq OWNED BY public.transactions.id;


--
-- TOC entry 3184 (class 2604 OID 35312)
-- Name: accounts id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts ALTER COLUMN id SET DEFAULT nextval('public.accounts_id_seq'::regclass);


--
-- TOC entry 3186 (class 2604 OID 35339)
-- Name: orders id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders ALTER COLUMN id SET DEFAULT nextval('public.orders_id_seq'::regclass);


--
-- TOC entry 3185 (class 2604 OID 35321)
-- Name: packets id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.packets ALTER COLUMN id SET DEFAULT nextval('public.packets_id_seq'::regclass);


--
-- TOC entry 3188 (class 2604 OID 35376)
-- Name: photographers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.photographers ALTER COLUMN id SET DEFAULT nextval('public.photographers_id_seq'::regclass);


--
-- TOC entry 3187 (class 2604 OID 35348)
-- Name: transactions id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions ALTER COLUMN id SET DEFAULT nextval('public.transactions_id_seq'::regclass);


--
-- TOC entry 3339 (class 0 OID 35309)
-- Dependencies: 210
-- Data for Name: accounts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.accounts (id, username, password, email, alamat, name, nomor_hp, role, created_at, updated_at) FROM stdin;
1	admin	admin	admin@mail.com	rahasia	admin	0809242421421	admin	2022-12-04 14:33:05.301515	2022-12-04 14:33:05.301515
3	coba	coba	coba@mail.com	\N	\N	\N	\N	\N	\N
2	jono	jono	jono@mail.com	jalan sumberdosa	Jono Kun	08996396305	\N	\N	2022-12-05 09:37:20.694434
\.


--
-- TOC entry 3343 (class 0 OID 35336)
-- Dependencies: 214
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.orders (id, username, id_paket, nama_booking, tanggal_booking, dp, status, created_at, updated_at) FROM stdin;
6	jono	1	Jono Sujono	2022-12-16	50000	rejected	2022-12-06 20:50:03.323777	2022-12-06 20:50:03.323777
5	jono	1	Jono Sujono	2022-12-16	50000	done	2022-12-06 14:53:13.15919	2022-12-06 14:53:13.15919
7	jono	1	Jono Sujono	2022-12-23	50000	paid	2022-12-06 21:47:16.201828	2022-12-06 21:47:16.201828
8	jono	2	Andi	2022-12-27	37500	pending	2022-12-06 22:07:33.815041	2022-12-06 22:07:33.815041
\.


--
-- TOC entry 3341 (class 0 OID 35318)
-- Dependencies: 212
-- Data for Name: packets; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.packets (id, jenis_paket, jenis_foto, ukuran_foto, keterangan, biaya) FROM stdin;
1	Fotografer	Cetak	20R	Paket foto Murah Meriah 1, dapatkan kualitas foto yang terbaik dengan fotografer yang memiliki skill terjamin dengan hasil foto cetak sebesar 20R	200000
2	Studio	SoftCopy	UHD	Paket Studio 1 dengan hasil foto yang UHD dan anda dapat memilih 2 gaya dengan total 5 foto	150000
\.


--
-- TOC entry 3347 (class 0 OID 35373)
-- Dependencies: 218
-- Data for Name: photographers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.photographers (id, nama, status, jadwal) FROM stdin;
2	Juki Pro Max	available	2022-12-16
1	Jono si Pro	available	2022-12-23
\.


--
-- TOC entry 3345 (class 0 OID 35345)
-- Dependencies: 216
-- Data for Name: transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transactions (id, orders_id, total, metode_pembayaran, status, created_at) FROM stdin;
4	5	50000	bank	pending	2022-12-06 20:49:33.444942
5	7	50000	ovo	pending	2022-12-06 21:59:45.11086
\.


--
-- TOC entry 3358 (class 0 OID 0)
-- Dependencies: 209
-- Name: accounts_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.accounts_id_seq', 3, true);


--
-- TOC entry 3359 (class 0 OID 0)
-- Dependencies: 213
-- Name: orders_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.orders_id_seq', 8, true);


--
-- TOC entry 3360 (class 0 OID 0)
-- Dependencies: 211
-- Name: packets_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.packets_id_seq', 2, true);


--
-- TOC entry 3361 (class 0 OID 0)
-- Dependencies: 217
-- Name: photographers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.photographers_id_seq', 2, true);


--
-- TOC entry 3362 (class 0 OID 0)
-- Dependencies: 215
-- Name: transactions_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transactions_id_seq', 5, true);


--
-- TOC entry 3190 (class 2606 OID 35316)
-- Name: accounts accounts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts_pkey PRIMARY KEY (id);


--
-- TOC entry 3194 (class 2606 OID 35343)
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);


--
-- TOC entry 3192 (class 2606 OID 35325)
-- Name: packets packets_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.packets
    ADD CONSTRAINT packets_pkey PRIMARY KEY (id);


--
-- TOC entry 3198 (class 2606 OID 35380)
-- Name: photographers photographers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.photographers
    ADD CONSTRAINT photographers_pkey PRIMARY KEY (id);


--
-- TOC entry 3196 (class 2606 OID 35352)
-- Name: transactions transactions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_pkey PRIMARY KEY (id);


-- Completed on 2022-12-06 22:28:34

--
-- PostgreSQL database dump complete
--

