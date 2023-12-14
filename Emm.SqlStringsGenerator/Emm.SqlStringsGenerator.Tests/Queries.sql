--namespace: Emm.SqlStringsGenerator.Sample
--class: MyQueries

--name: GetCount
SELECT COUNT(*) FROM Table

--name: DeleteQuery
DELETE FROM TABLE WHERE Id = 1


-- this is a regular comment
-- another regular sql comment
-- this: looks like a directive, but its not
--
--\n\t