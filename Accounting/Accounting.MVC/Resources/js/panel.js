const delete_item_btn = document.querySelectorAll('.delete-item-btn');
const current_url = window.location.href;
const form = document.querySelector('form');

delete_item_btn.forEach(element => {
    element.addEventListener('click', (e) => {

        console.log('clicked');
        e = e || window.event;
        let target = e.target || e.srcElement;
        let id = target.parentNode.childNodes[1].textContent;
        let delete_url = current_url + `/Delete/${id}`;
        console.log(delete_url);

        fetch(delete_url, {
            method: 'DELETE', // *GET, POST, PUT, DELETE, etc.
            credentials: 'include',
            headers: {
                'Content-Type': 'Application/x-www-form-urlencoded',
            },
            redirect: 'follow'
        }).then((res) => {
            if (res.ok) {
                window.location.reload();
            }
            //used only for spec foreign key conflict
            else if (res.status === 450) {
                console.log(res.statusText);
            }
        });
    });
});

/* add new spec row on click*/
let add_row_btn = document.getElementById('add-row-btn');
let add_col_btn = document.getElementById('add-col-btn');

/* remove new spec row onclick*/
let remove_row_btn = document.getElementById('remove-row-btn');
let remove_col_btn = document.getElementById('remove-col-btn');

let spec_form = document.getElementById('spec-form');
let spec_add_item_group = document.getElementById('spec-add-item-group');

function removeSpecRow() {
    let all_rows = document.querySelectorAll('.row-parent');
    if (all_rows.length > 2) {
        let node_to_remove = all_rows[all_rows.length - 1];
        node_to_remove.remove();
    }
}

function removeSpecCol() {
    let all_rows = document.querySelectorAll('.row-parent');
    if (all_rows[0].childElementCount > 2) {
        all_rows.forEach(row => {
            let node_to_remove = row.lastElementChild;
            node_to_remove.remove();
        });
    }
}

function addSpecRow() {
    let row_count = document.querySelectorAll('row-parent').childElementCount;

    let last_row = spec_add_item_group.previousElementSibling;
    let spec_row = last_row.cloneNode(true);
    spec_row.firstChild.className = `form-control column-toggle-${row_count + 1}`;
    spec_form.insertBefore(spec_row, spec_add_item_group);

    addListener();
}

function addSpecCol() {
    let all_rows = document.querySelectorAll('.row-parent');

    let first_row = document.querySelector('.row-parent');
    let col_count = first_row.childElementCount;

    col_count += 1;
    all_rows.forEach(row => {
        let last_node = row.lastElementChild;
        let new_node = last_node.cloneNode(true);

        let node_name = new_node.lastElementChild.getAttribute('name');
        if (node_name === 'First[]') {
            new_node.lastElementChild.className = 'form-control column-' + col_count;
        }
        else {
            new_node.lastElementChild.className = 'form-control column-toggle-' + col_count;
        }
        row.appendChild(new_node);

    });
    addListener();
}

if (add_row_btn !== null) {
    add_row_btn.addEventListener('click', (e) => {
        console.log('clicked')
        addSpecRow();
    });
}

if (add_col_btn !== null) {
    add_col_btn.addEventListener('click', (e) => {
        console.log('clicked')
        addSpecCol();
    });
}

if (remove_row_btn !== null) {
    remove_row_btn.addEventListener('click', (e) => {
        console.log('clicked')
        removeSpecRow();
    });
}

if (remove_col_btn !== null) {
    remove_col_btn.addEventListener('click', (e) => {
        console.log('clicked')
        removeSpecCol();
    });
}

//add eventListener to all master nodes
function addListener(element = null) {
    let parent_el = document.querySelector('.row-parent');
    let num_of_cols = parent_el.childElementCount;

    for (let i = 1; i <= num_of_cols; i++) {
        let master_node = document.querySelector(`.column-${i}`);
        let slave_nodes = document.querySelectorAll(`.column-toggle-${i}`);

        master_node.addEventListener('keyup', (e) => {
            if (e.target.value === '') {
                slave_nodes.forEach(el => {
                    el.disabled = true;
                    el.style.opacity = 0.4;
                });
            }
            else {
                slave_nodes.forEach(el => {
                    el.disabled = false;
                    el.style.opacity = 1
                });
            }
        });
    }

}

function closeAlert() {
    $('.alert').alert('close');
}

//exec once on js load
if (add_row_btn) {
    addListener();
}

/* VARIATIONS CONTROL */
let add_option_btn = document.querySelector('.add-option-btn');

if (add_option_btn != null) {
    let remove_option_btn = document.querySelector('.remove-option-btn');
    let add_variation_btn = document.querySelector('.add-variation-btn');
    let remove_variation_btn = document.querySelector('.remove-variation-btn');

    addOptionListener(add_option_btn, remove_option_btn);
}
function addOptionListener(element_add = null, element_remove = null) {

    if (element_remove !== null) {
        element_remove.addEventListener('click', (e) => {
            console.log('clicked');
            let parent_container = e.target.parentNode.parentNode;
            let parent_el = e.target.parentNode;
            if (parent_container.childElementCount > 3) {
                parent_el.previousElementSibling.remove();
                console.log(parent_el.previousElementSibling);
            }
        });
    }
    if (element_add !== null) {
        element_add.addEventListener('click', (e) => {
            let parent_container = element_add.parentNode.parentNode;
            let ref_el = element_add.parentNode;
            let new_option_el = document.createElement('div');
            new_option_el.innerHTML = `<div>
                <input type="text" class="form-control" name="Options[]" placeholder="Option value" required>
            </div>`;
            parent_container.insertBefore(new_option_el, ref_el);

        });
    }
}
function addVariationListener(element_add = null, element_remove = null,) {
    if (element_add !== null) {
        element_add.addEventListener('click', (e) => {
            let parent_container = element_add.parentNode.parentNode;
            let ref_element = element_add.parentNode;

            let new_el = document.createElement('div');
            new_el.innerHTML = `<label>Variation</label>
            <div>
                <input type="text" class="form-control" name="VariationNames[]" placeholder="Variation name">
            </div>
            <div>
                <input type="text" class="form-control" name="VariationValues[]" placeholder="Option value">
            </div>
            <div class="option-control">
                <button type="button" class="btn btn-secondary add-option-btn"><i class="fas fa-plus pr-2"></i>Add option</button>
                <button type="button" class="btn btn-secondary remove-option-btn"><i class="fas fa-plus pr-2"></i>Remove option</button>
            </div>`;

            parent_container.insertBefore(new_el, ref_element);

            let add_btn = new_el.querySelector('.add-option-btn');
            let remove_btn = new_el.querySelector('.remove-option-btn');

            addOptionListener(add_btn, remove_btn);

        });
    }
    
    if (element_remove !== null) {
        element_remove.addEventListener('click', (e) => {
            let container_count = e.target.parentNode.parentNode.childElementCount;
            if (container_count > 5) {
                let parent_el = e.target.parentNode;
                parent_el.previousElementSibling.remove();
            }
            else {

            }
        });
    }
}

document.addEventListener("DOMContentLoaded", function () {
    let nodes = document.querySelectorAll(".nav-item");
    let controllerName =
        window.location.pathname
            .split("/")[1]
            .replaceAll("/", "")
            .toLowerCase();

    let menuLinks = document.querySelectorAll(".nav-pills .nav-link p");
    menuLinks.forEach(node => {
        let nodeText =
            node.dataset
                .controller
                ?.trim()
                ?.replaceAll(" ", "")
                ?.toLowerCase();

        if(nodeText === controllerName){
            SetParentState(node);
        }
    })


});

function SetParentState(node){
    if(node === null || node.nodeName === 'NAV'){
        return;
    }
    if(node.classList.contains('nav-item')){
        node.classList.add('menu-open');
        node.querySelector('.nav-link').classList.add('active');
        let treeView = node.querySelector('.nav-treeview');
        if(treeView !== null) {
            treeView.style.display = 'block'
        }
    }
    return SetParentState(node.parentElement)
}


