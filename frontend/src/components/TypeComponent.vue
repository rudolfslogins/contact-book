<template>
    <md-autocomplete 
        id="type" v-model="selectedType" 
        @md-selected="handleTypeSelection"
        :md-options="types.map(x=>({
                    'typeName': x.typeName,
                    'toLowerCase':()=>x.typeName.toLowerCase(),
                    'toString':()=>x.typeName
                    }))"  
        @md-changed="getTypes" 
        @md-opened="getTypes" 
        md-dense required>
        <label>Type</label>
            <template slot="md-autocomplete-item" slot-scope="{ item, term }">
                <md-highlight-text :md-term="term">{{ item.typeName }}</md-highlight-text>
            </template>
    </md-autocomplete>
</template>

<script>
import ContactPage from "../views/ContactPage"
export default {
    name: "TypeComponent",
    props: {
        componentType: {}
    },
    data(){
        return {
            types: [],
            selectedType: null
        }
    },
    methods:{
        async getTypes (searchTerm) {
            let searchRes = await this.$http.get(`http://localhost:8082/api/types`);
            this.types = await searchRes.json();
        },
        handleTypeSelection(event) {
            this.$emit('typeUpdated', event.typeName);
            this.selectedType = event.typeName;
        }
    },
    mounted(){
        this.getTypes();
        this.selectedType = this.componentType;
    }
}
</script>

<style>

</style>